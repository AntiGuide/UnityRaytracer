using System.Collections.Generic;
using UnityEngine;

public class Lambert : Material {
    private const float AMBIENT = 1f;
    private const float DIFFUSE = 0f;

    public Lambert(Shape parent) : base(parent) { }

    public override Color CalculateColorSphere(Scene scene, Vector3 intersectPoint, List<Light> lights) {
        return CalculateColor(scene, intersectPoint, lights, (intersectPoint - parent.position).normalized);
    }

    public override Color CalculateColorPlane(Scene scene, Vector3 intersectPoint, List<Light> lights, Vector3 normal) {
        return CalculateColor(scene, intersectPoint, lights, normal);
    }

    private Color CalculateColor(Scene scene, Vector3 intersectPoint, List<Light> lights, Vector3 normal) {
        var retColor = new Color();

        foreach (var light in lights) {
            var collision = false;
            var lightPosition = light.position;
            scene.shapeList.ForEach(s => {
                if (s.Equals(parent)) return;
                
                var intersectToLight = new Ray(intersectPoint, lightPosition - intersectPoint);
                var intersectAtLength = s.Intersect(intersectToLight) ?? float.MaxValue;
                var magnitudeLight = Vector3.Magnitude(lightPosition - intersectPoint);
                collision = intersectAtLength < magnitudeLight;
            });

            var IAmbient = GetColor * AMBIENT;
            var lightIntensity = light.Intensity;
            if (collision) {
                retColor += IAmbient * lightIntensity;
            } else {
                var lightVec = (lightPosition - intersectPoint).normalized;
                var cosAlpha = Mathf.Clamp01(Vector3.Dot(normal, lightVec));
                var IDiffuse = GetColor * (cosAlpha * DIFFUSE);
                retColor += (IAmbient + IDiffuse) * lightIntensity;
            }
        }

        return new Color(Mathf.Clamp01(retColor.r), Mathf.Clamp01(retColor.g), Mathf.Clamp01(retColor.b));
    }
}