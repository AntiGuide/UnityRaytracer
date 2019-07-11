using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

public class Lambert : Material {
    private const float AMBIENT = 0.3f;
    private const float DIFFUSE = 0.7f;

    public Lambert(Shape parent/*, float ambient, float diffuse*/) : base(parent) {
        /*this.ambient = ambient;
        this.diffuse = diffuse;*/
    }

    public override Color CalculateColorSphere(Scene scene, Vector3 intersectPoint, List<Light> lights) {
        if (lights.Count == 0) {
            throw new ArgumentException();
        }
        
        var colors = new List<Color>(lights.Count);

        foreach (var light in lights) {
            var collisionCheckObjects = scene.shapeList.FindAll(s => s != parent);
            var collision = false;
            collisionCheckObjects.ForEach(s => {
                var intersectToLight = new Ray(intersectPoint, light.position - intersectPoint);
                var intersectAtLength = s.Intersect(intersectToLight) ?? float.MaxValue;
                var magnitudeLight = Vector3.Magnitude(light.position - intersectPoint);
                collision = intersectAtLength < magnitudeLight;
            });


            var normal = (intersectPoint - parent.position).normalized;
            var lightVec = (light.position - intersectPoint).normalized;

            var IAmbient = GetColor * AMBIENT;
            var cosAlpha = Mathf.Clamp01(Vector3.Dot(normal, lightVec));
            var IDiffuse = GetColor * (cosAlpha * DIFFUSE);
            if (collision) {
                colors.Add(IAmbient * light.Intensity);
            } else {
                colors.Add((IAmbient + IDiffuse) * light.Intensity);
            }
        }
        
        var retColor = new Color();

        foreach (var color in colors) {
            retColor += color;
        }

        retColor = new Color(Mathf.Clamp01(retColor.r), Mathf.Clamp01(retColor.g), Mathf.Clamp01(retColor.b));
        
        if (retColor == Color.black) {
            Debug.Log("");
        }
        
        return new Color(Mathf.Clamp01(retColor.r), Mathf.Clamp01(retColor.g), Mathf.Clamp01(retColor.b));
    }
}