﻿using System.Collections.Generic;
using UnityEngine;

public class Phong : Material {
    private const float AMBIENT = 0.05f;
    private const float DIFFUSE = 0.95f;
    
    private readonly float reflection;
    private readonly float shiny;
    
    public Phong(Shape parent) : base(parent) {
        reflection = 0.8f;
        shiny = 30f;
    }
    
    public Phong(Shape parent, float reflection, float shiny) : base(parent) {
        this.reflection = reflection;
        this.shiny = shiny;
    }

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
            foreach (var s in scene.shapeList) {
                if (s.Equals(parent)) continue;
                
                var intersectToLight = new Ray(intersectPoint, lightPosition - intersectPoint);
                var intersectAtLength = s.Intersect(intersectToLight) ?? float.MaxValue;
                var magnitudeLight = Vector3.Magnitude(lightPosition - intersectPoint);
                collision = intersectAtLength < magnitudeLight;
                if (collision) break;
            }

            var lightVec = (lightPosition - intersectPoint).normalized;
            var cosAlpha = Mathf.Clamp01(Vector3.Dot(normal, lightVec));
            var IAmbient = GetColor * AMBIENT;
            var lightIntensity = light.Intensity;
            if (collision || cosAlpha <= float.Epsilon) {
                retColor += IAmbient * lightIntensity;
            } else {
                var V = (scene.Camera.position - intersectPoint).normalized;                    //V  = Materialpunkt zu Kamera
                var L = (light.position - intersectPoint).normalized;                           //L  = Materialpunkt zu Licht
                var R = (2*Mathf.Clamp01(Vector3.Dot(normal,L))*normal-L).normalized;           //R  = L an N gespiegelt => 2*(N*L)*N-L
                var Specular = reflection * Mathf.Pow(Vector3.Dot(R, V), shiny) * light.color;  //Specular = ks * (Mathf.Dot(R * V) ^ a) * is
                var IDiffuse = GetColor * (cosAlpha * DIFFUSE);
                retColor += (IAmbient + IDiffuse + Specular) * lightIntensity;
            }
        }

        return new Color(Mathf.Clamp01(retColor.r), Mathf.Clamp01(retColor.g), Mathf.Clamp01(retColor.b));
    }
}
