using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;

public class Plane : Shape {
    private readonly Vector3 normal;
    private float q;

    public Plane(Vector3 position, Vector3 normal, Color color) : base(position, color) {
        this.normal = normal;
        //this.material = new Lambert(this);
        Material = new Phong(this);
    }

    public override float? Intersect(Ray ray) {
        //ray = ray.TransformRay(worldToLocalMatrix);
        
        if (Math.Abs(Vector3.Dot(normal, ray.direction)) < float.Epsilon) return null;

        q = Vector3.Dot(Position, normal);
        var t = T(normal, ray.origin, q ,ray.direction);

        if (t < 0) {
            return null;
        }
        
        return t;
    }

    public override Color CalculateColor(Scene scene, Vector3 intersectPoint, List<Light> list) {
        return Material.CalculateColorPlane(scene, intersectPoint, list, normal);
    }

    private static float T(Vector3 pn, Vector3 p0, float q, Vector3 d) => (Vector3.Dot(pn,p0) + q) / Vector3.Dot(pn,d);
    
    public PlaneData CreateStructFromObject() {
        return new PlaneData(normal, Position, Color);
    }
}

[BurstCompile]
public struct PlaneData {
    private readonly Vector3 normal;
    private float q;
    public Vector3 Position;
    public readonly Color Color;
    public Matrix4x4 WorldToLocalMatrix;
    
    private const float AMBIENT = 0.3f;
    private const float DIFFUSE = 0.7f;
    private const float REFLECTION = 0.8f;
    private const float SHINY = 30f;

    public PlaneData(Vector3 normal, Vector3 position, Color color) : this() {
        this.normal = normal;
        Position = position;
        Color = color;
        
//        var gameObject = new GameObject();
//        gameObject.transform.position = Position;
//        var objectTransform = gameObject.transform;
//        WorldToLocalMatrix = objectTransform.worldToLocalMatrix;
    }

    public float? Intersect(Ray ray) {
        //ray = ray.TransformRay(worldToLocalMatrix);
        
        if (Math.Abs(Vector3.Dot(normal, ray.direction)) < float.Epsilon) return null;

        q = Vector3.Dot(Position, normal);
        var t = T(normal, ray.origin, q ,ray.direction);

        if (t < 0) {
            return null;
        }
        
        return t;
    }

    public Color CalculateColor(NativeArray<SphereData> sceneSpheres, NativeArray<PlaneData> scenePlanes, Vector3 cameraPosition, Vector3 intersectPoint, NativeArray<LightData> lights) {
        var retColor = new Color();

        for (var index = 0; index < lights.Length; index++) {
            var light = lights[index];
            var collision = false;
            var lightPosition = light.Position;
            for (var i = 0; i < sceneSpheres.Length; i++) {
                var s = sceneSpheres[i];
                var intersectToLight = new Ray(intersectPoint, lightPosition - intersectPoint);
                var intersectAtLength = s.Intersect(intersectToLight) ?? float.MaxValue;
                var magnitudeLight = Vector3.Magnitude(lightPosition - intersectPoint);
                collision = intersectAtLength < magnitudeLight;
            }

            for (var i = 0; i < scenePlanes.Length; i++) {
                var s = scenePlanes[i];
                if (s.Position == Position) continue;

                var intersectToLight = new Ray(intersectPoint, lightPosition - intersectPoint);
                var intersectAtLength = s.Intersect(intersectToLight) ?? float.MaxValue;
                var magnitudeLight = Vector3.Magnitude(lightPosition - intersectPoint);
                collision = intersectAtLength < magnitudeLight;
            }

            var lightVec = (lightPosition - intersectPoint).normalized;
            var cosAlpha = Mathf.Clamp01(Vector3.Dot(normal, lightVec));
            var ambient = Color * AMBIENT;
            var lightIntensity = light.Intensity;
            if (collision || cosAlpha <= float.Epsilon) {
                retColor += ambient * lightIntensity;
            }
            else {
                var v = (cameraPosition - intersectPoint).normalized; //V  = Materialpunkt zu Kamera
                var l = (light.Position - intersectPoint).normalized; //L  = Materialpunkt zu Licht
                var r = (2 * Mathf.Clamp01(Vector3.Dot(normal, l)) * normal - l)
                    .normalized; //R  = L an N gespiegelt => 2*(N*L)*N-L
                var specular =
                    REFLECTION * Mathf.Pow(Vector3.Dot(r, v), SHINY) *
                    light.Color; //Specular = ks * (Mathf.Dot(R * V) ^ a) * is
                var diffuse = Color * (cosAlpha * DIFFUSE);
                retColor += (ambient + diffuse + specular) * lightIntensity;
            }
        }

        return new Color(Mathf.Clamp01(retColor.r), Mathf.Clamp01(retColor.g), Mathf.Clamp01(retColor.b));
    }

    private static float T(Vector3 pn, Vector3 p0, float q, Vector3 d) => (Vector3.Dot(pn,p0) + q) / Vector3.Dot(pn,d);
}
