using UnityEngine;

public class Raytracer {
    private readonly Scene scene;
    private readonly Color backgroundColor;
    private readonly Camera camera;

    public Raytracer(Scene scene, Color backgroundColor, Camera camera) {
        this.scene = scene;
        this.backgroundColor = backgroundColor;
        this.camera = camera;
    }

    public Color[] Render() {
        var ret = new Color[camera.xMax * camera.yMax];
        
        for(var y = 0;y < camera.yMax; y++) {
            for (var x = 0; x < camera.xMax; x++) {
                var tmpVec = camera.CalculateDestinationPoint(x, y);
                var tmpRay = new Ray(camera.position, tmpVec);
                var tmpColor = backgroundColor;
                var selectedT = float.MaxValue;
                foreach (var s in scene.shapeList) {
                    var t = s.Intersect(tmpRay);
                    if (t == null) continue;
                    if (!(t < selectedT)) continue;
                    
                    selectedT = (float) t;
                    tmpColor = s.CalculateColor(scene, tmpRay.GetPoint(selectedT), scene.lightList);
                }

                var cameraXMax = camera.xMax;
                var selectY = y;
                var writeY = selectY * cameraXMax;
                var writeX = camera.xMax - 1 - x;
                ret[writeY + writeX] = tmpColor;
            }
        }
        
        return ret;
    }
}