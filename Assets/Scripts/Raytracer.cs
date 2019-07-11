using System;
using System.Threading.Tasks;
using UnityEngine;

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
        var ret = new Color[(camera.xMax + 1) * (camera.yMax + 1)];
        //var dt1 = DateTime.Now;

        Parallel.For(0, camera.yMax, y => {
            Parallel.For(0, camera.xMax, x => {
                var tmpVec = camera.CalculateDestinationPoint(x, y);
                var tmpRay = new Ray(camera.position, tmpVec);
                var tmpColor = backgroundColor;
                var selectedT = float.MaxValue;
                foreach (var s in scene.shapeList) {
                    var t = s.Intersect(tmpRay);
                    
                    if (t == null) continue;
                    if (!(t < selectedT)) continue;
                    
                    selectedT = (float) t;
                    tmpColor = s.color;
                }

                ret[(camera.yMax - y) * (camera.xMax + 1) + x] = tmpColor;
            });
        });

//        var dt2 = DateTime.Now;
//        Debug.Log((dt2 - dt1).Milliseconds);
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