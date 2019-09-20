using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using Sirenix.Utilities;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Raytracer {
    private const int JOB_COUNT = 12;
    private Scene scene;
    private RenderWindow renderWindow;
    private int maxRecursions;
    private Color backgroundColor;
    private Camera camera;
    public JobHandle[] JobHandles;
    
    private NativeArray<CameraData> cameraDataNativeArray;
    private NativeArray<Color> backgroundColorNativeArray;
    private NativeArray<SphereData> sceneSpheresNativeArray;
    private NativeArray<PlaneData> scenePlanesNativeArray;
    private NativeArray<LightData> sceneLightsNativeArray;
    private NativeArray<Color>[] resultBitmapNativeArrays;

    public Raytracer(Scene scene, RenderWindow renderWindow, int maxRecursions, Color backgroundColor, Camera camera) {
        this.scene = scene;
        this.renderWindow = renderWindow;
        this.maxRecursions = maxRecursions;
        this.backgroundColor = backgroundColor;
        this.camera = camera;
    }

    public void Render() {
        cameraDataNativeArray = new NativeArray<CameraData>(new [] { camera.CreateStructFromObject() }, Allocator.Persistent);
        backgroundColorNativeArray = new NativeArray<Color>(new [] { backgroundColor }, Allocator.Persistent);
        sceneSpheresNativeArray = new NativeArray<SphereData>(scene.SphereList.Select(s => s.CreateStructFromObject()).ToArray(), Allocator.Persistent);
        scenePlanesNativeArray = new NativeArray<PlaneData>(scene.PlaneList.Select(p => p.CreateStructFromObject()).ToArray(), Allocator.Persistent);
        sceneLightsNativeArray = new NativeArray<LightData>(scene.LightList.Select(l => l.CreateStructFromObject()).ToArray(), Allocator.Persistent);
        
        resultBitmapNativeArrays = new NativeArray<Color>[JOB_COUNT];
        JobHandles = new JobHandle[JOB_COUNT];
        var remainingY = camera.YMax;
        var remainingJobs = JOB_COUNT;
        var offset = 0;
        for (var i = 0; i < JOB_COUNT; i++) {
            var thisY = Mathf.CeilToInt(remainingY / (float) remainingJobs);
            remainingJobs--;
            remainingY -= thisY;
            var bitmapSize = (camera.XMax + 1) * thisY;
            resultBitmapNativeArrays[i] = new NativeArray<Color>(bitmapSize , Allocator.Persistent);
            var raytraceJob = new RaytracePixelJob {
                CameraData = cameraDataNativeArray,
                BackgroundColor = backgroundColorNativeArray,
                SphereDatas = sceneSpheresNativeArray,
                PlanesDatas = scenePlanesNativeArray,
                LightDatas = sceneLightsNativeArray,
                ResultBitmapNativeArray = resultBitmapNativeArrays[i],
                Offset = offset,
            };

            offset += bitmapSize;
            
            if (i == 0) {
                JobHandles[i] = raytraceJob.Schedule(bitmapSize, bitmapSize / 12);
            } else {
                JobHandles[i] = raytraceJob.Schedule(bitmapSize, bitmapSize / 12, JobHandles[i-1]);
            }
        }

        
    }

    public IEnumerator RenderComplete() {
        var offset = 0;
        for (var i = 0; i < JobHandles.Length; i++) {
            var jh = JobHandles[i];
            yield return new WaitUntil(() => jh.IsCompleted);
            jh.Complete();
            var arr = resultBitmapNativeArrays[i].ToArray();
            renderWindow.SetPixels(arr, offset);
            offset += arr.Length;
        }
        
        cameraDataNativeArray.Dispose();
        backgroundColorNativeArray.Dispose();
        sceneSpheresNativeArray.Dispose();
        scenePlanesNativeArray.Dispose();
        sceneLightsNativeArray.Dispose();
        resultBitmapNativeArrays.ForEach(na => na.Dispose());
    }

    public Color SendPrimaryRay() {
        throw new NotImplementedException();
    }

    public Color TraceRay() {
        throw new NotImplementedException();
    }

    public Color Shade() {
        throw new NotImplementedException();
    }

    public Color TraceIllumination() {
        throw new NotImplementedException();
    }
}

[BurstCompile]
public struct RaytracePixelJob : IJobParallelFor {
    [ReadOnly] public NativeArray<CameraData> CameraData;
    [ReadOnly] public NativeArray<Color> BackgroundColor;
    [ReadOnly] public NativeArray<SphereData> SphereDatas;
    [ReadOnly] public NativeArray<PlaneData> PlanesDatas;
    [ReadOnly] public NativeArray<LightData> LightDatas;
    [ReadOnly] public int Offset;
    
    [WriteOnly] public NativeArray<Color> ResultBitmapNativeArray;
    
    public void Execute(int index) {
        var x = (index + Offset) % (CameraData[0].XMax + 1);
        var y = (index + Offset) / (CameraData[0].XMax + 1);
        
        var tmpVec = CalculateDestinationPoint(x, y);
        var tmpRay = new Ray(CameraData[0].Position, tmpVec);
        var tmpColor = BackgroundColor[0];
        var selectedT = float.MaxValue;

        for (var i = 0; i < PlanesDatas.Length; i++) {
            var s = PlanesDatas[i];
            var t = s.Intersect(tmpRay);

            if (t == null) continue;
            if (!(t < selectedT)) continue;

            selectedT = (float) t;
            tmpColor = s.CalculateColor(SphereDatas, PlanesDatas, CameraData[0].Position, tmpRay.GetPoint(selectedT),
                LightDatas);
        }

        for (var i = 0; i < SphereDatas.Length; i++) {
            var s = SphereDatas[i];
            var t = s.Intersect(tmpRay);

            if (t == null) continue;
            if (!(t < selectedT)) continue;

            selectedT = (float) t;
            tmpColor = s.CalculateColor(SphereDatas, PlanesDatas, CameraData[0].Position, tmpRay.GetPoint(selectedT),
                LightDatas);
        }

        ResultBitmapNativeArray[index] = tmpColor;
    }
    
    private Vector3 CalculateDestinationPoint(int x, int y) {
        var normalizedPos = new Vector2 {
            x = 2f * ((x + 0.5f) / (CameraData[0].XMax + 1)) - 1f,
            y = 2f * ((y + 0.5f) / (CameraData[0].YMax + 1)) - 1f
        };

        var p1 = CameraData[0].H * normalizedPos.y * CameraData[0].UpVector + CameraData[0].W * normalizedPos.x * CameraData[0].SideVector;
        return (p1 + CameraData[0].LookAt).normalized;
    }
}