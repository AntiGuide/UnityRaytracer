using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Sirenix.Utilities;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Raytracer {
    private Scene scene;
    private RenderWindow renderWindow;
    private int maxRecursions;
    private Color backgroundColor;
    private Camera camera;

    public Raytracer(Scene scene, RenderWindow renderWindow, int maxRecursions, Color backgroundColor, Camera camera) {
        this.scene = scene;
        this.renderWindow = renderWindow;
        this.maxRecursions = maxRecursions;
        this.backgroundColor = backgroundColor;
        this.camera = camera;
    }

    public Color[] Render() {
        var stopwatch = new Stopwatch[10];
        for (var i = 0; i < stopwatch.Length; i++) {
            stopwatch[i] = new Stopwatch();
        }
        stopwatch[0].Start();
        
        var ret = new Color[(camera.xMax + 1) * (camera.yMax + 1)];
//        Parallel.For(0, camera.yMax + 1, y => {
//            Parallel.For(0, camera.xMax + 1, x => {
//                var tmpVec = camera.CalculateDestinationPoint(x, y);
//                var tmpRay = new Ray(camera.position, tmpVec);
//                var tmpColor = backgroundColor;
//                var selectedT = float.MaxValue;
//                foreach (var s in scene.shapeList) {
//                    var t = s.Intersect(tmpRay);
//                    
//                    if (t == null) continue;
//                    if (!(t < selectedT)) continue;
//                    
//                    selectedT = (float) t;
//                    tmpColor = s.CalculateColor(scene, tmpRay.GetPoint(selectedT), scene.lightList);
//                }
//
//                var cameraXMax = camera.xMax + 1;
//                var selectY = (camera.yMax - y);
//                var writeY = selectY * cameraXMax;
//                var writeX = x;
//                ret[writeY + writeX] = tmpColor;
//            });
//        });
        
        for(int y = 0;y < camera.yMax + 1; y++) {
            for (int x = 0; x < camera.xMax + 1; x++) {
                //stopwatch[1].Start();
                var tmpVec = camera.CalculateDestinationPoint(x, y);
                //stopwatch[1].Stop();
                var tmpRay = new Ray(camera.position, tmpVec);
                var tmpColor = backgroundColor;
                var selectedT = float.MaxValue;
                foreach (var s in scene.shapeList) {
                    var t = s.Intersect(tmpRay);
                    
                    if (t == null) continue;
                    if (!(t < selectedT)) continue;
                    
                    selectedT = (float) t;
                    //stopwatch[2].Start();
                    tmpColor = s.CalculateColor(scene, tmpRay.GetPoint(selectedT), scene.lightList);
                    //stopwatch[2].Stop();
                    
                }

                var cameraXMax = camera.xMax + 1;
                var selectY = (camera.yMax - y);
                var writeY = selectY * cameraXMax;
                var writeX = x;
                ret[writeY + writeX] = tmpColor;
            }
        }
        
        stopwatch[0].Stop();
        Debug.LogFormat("Iteration for all pixels took {0:hh\\:mm\\:ss\\:fffff}", stopwatch[0].Elapsed);
        var SinglePixelTime = new TimeSpan(stopwatch[0].Elapsed.Ticks / (camera.yMax * camera.xMax));
        //var SinglePixelTimeDestPoint = new TimeSpan(stopwatch[1].Elapsed.Ticks / (camera.yMax * camera.xMax));
        //var SinglePixelTimeCalcColor = new TimeSpan(stopwatch[2].Elapsed.Ticks / (camera.yMax * camera.xMax));
        Debug.LogFormat("Iteration for one pixel took {0:fffffff} on average", SinglePixelTime);
        //Debug.LogFormat("Iteration for DestPoint took {0:fffffff} on average", SinglePixelTimeDestPoint);
        //Debug.LogFormat("Iteration for CalcColor took {0:fffffff} on average", SinglePixelTimeCalcColor);
        return ret;
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