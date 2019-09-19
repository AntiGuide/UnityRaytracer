using System.Collections.Generic;
using UnityEngine;

public class Phong : Material {
    
    private const float AMBIENT = 0.3f;
    private const float DIFFUSE = 0.7f;
    
    //ks = Reflektions-Konstante für Specular Lighting (REFLECTION)
    private const float REFLECTION = 0.8f;
    
    //Wegen Unebenheit a als Shininess-Konstante
    //SHINY(a) = Shininess-Konstante
    private const float SHINY = 30f;
    
    public Phong(Shape parent) : base(parent) {
    }

    public override Color CalculateColorSphere(Scene scene, Vector3 intersectPoint, List<Light> lights) {
        return CalculateColor(scene, intersectPoint, lights, (intersectPoint - Parent.Position).normalized);
    }

    public override Color CalculateColorPlane(Scene scene, Vector3 intersectPoint, List<Light> lights, Vector3 normal) {
        return CalculateColor(scene, intersectPoint, lights, normal);
    }
    
    private Color CalculateColor(Scene scene, Vector3 intersectPoint, List<Light> lights, Vector3 normal) {
        var retColor = new Color();

        foreach (var light in lights) {
            var collision = false;
            var lightPosition = light.Position;
            foreach (var s in scene.ShapeList) {
                if (s.Equals(Parent)) continue;
                
                var intersectToLight = new Ray(intersectPoint, lightPosition - intersectPoint);
                var intersectAtLength = s.Intersect(intersectToLight) ?? float.MaxValue;
                var magnitudeLight = Vector3.Magnitude(lightPosition - intersectPoint);
                collision = intersectAtLength < magnitudeLight;
                //if (collision) break;
            }

            var lightVec = (lightPosition - intersectPoint).normalized;
            var cosAlpha = Mathf.Clamp01(Vector3.Dot(normal, lightVec));
            var ambient = GetColor * AMBIENT;
            var lightIntensity = light.Intensity;
            if (collision || cosAlpha <= float.Epsilon) {
                retColor += ambient * lightIntensity;
            } else {
                var v = (scene.Camera.Position - intersectPoint).normalized; //V  = Materialpunkt zu Kamera
                var l = (light.Position - intersectPoint).normalized; //L  = Materialpunkt zu Licht
                var r = (2*Mathf.Clamp01(Vector3.Dot(normal,l))*normal-l).normalized; //R  = L an N gespiegelt => 2*(N*L)*N-L
                var specular = REFLECTION * Mathf.Pow(Vector3.Dot(r, v), SHINY) * light.Color; //Specular = ks * (Mathf.Dot(R * V) ^ a) * is
                var diffuse = GetColor * (cosAlpha * DIFFUSE);
                retColor += (ambient + diffuse + specular) * lightIntensity;
            }
        }

        return new Color(Mathf.Clamp01(retColor.r), Mathf.Clamp01(retColor.g), Mathf.Clamp01(retColor.b));
    }
}
