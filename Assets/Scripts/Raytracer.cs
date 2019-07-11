using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
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
//                    tmpColor = s.color;
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
                var tmpVec = camera.CalculateDestinationPoint(x, y);
                var tmpRay = new Ray(camera.position, tmpVec);
                var tmpColor = backgroundColor;
                var selectedT = float.MaxValue;
                foreach (var s in scene.shapeList) {
                    var t = s.Intersect(tmpRay);
                    
                    if (t == null) continue;
                    if (!(t < selectedT)) continue;
                    
                    selectedT = (float) t;
                    //tmpColor = s.color;
                    tmpColor = s.CalculateColor(scene, tmpRay.GetPoint(selectedT), scene.lightList);
                    
                }

                var cameraXMax = camera.xMax + 1;
                var selectY = (camera.yMax - y);
                var writeY = selectY * cameraXMax;
                var writeX = x;
                ret[writeY + writeX] = tmpColor;
            }
        }
        
        stopwatch.Stop();
        Debug.LogFormat("Iteration for all pixels took {0:hh\\:mm\\:ss\\:fffff}", stopwatch.Elapsed);
        Debug.LogFormat("Iteration for one pixel took {0:hh\\:mm\\:ss\\:fffff} on average", new TimeSpan(stopwatch.Elapsed.Ticks / (camera.yMax * camera.xMax)));
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