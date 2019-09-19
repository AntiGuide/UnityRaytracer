using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;

public class Sphere : Shape {
    private const float RADIUS = 1f;

    public Sphere(Vector3 position, Color color) : base(position, color) {
        //this.material = new Lambert(this);
        Material = new Phong(this);
    }

    public override float? Intersect(Ray ray) {
        ray = ray.TransformRay(WorldToLocalMatrix);
        
        var o = ray.origin;
        var d = ray.direction;

        var b = 2f * (o.x * d.x + o.y * d.y + o.z * d.z);

        var c = o.x * o.x + o.y * o.y + o.z * o.z - RADIUS * RADIUS;

        if (Determinant(b, c) < 0f) {
            return null;
        }

        if (Math.Abs(Determinant(b, c)) < float.Epsilon) {
            var intersect = T0(b, c);
            if (intersect < 0) {
                return null;
            }
            
            return intersect;
        }

        var tList = new List<float>(2) {
            T0(b, c),
            T1(b, c)
        };
        
        tList.RemoveAll(f => f < 0f);
        if (tList.Count == 0) {
            return null;
        }
        
        return tList.Min();
    }

    public override Color CalculateColor(Scene scene, Vector3 intersectPoint, List<Light> list) {
        return Material.CalculateColorSphere(scene, intersectPoint, list);
    }

    private static float T0(float b, float c) => (-b-Mathf.Sqrt(Determinant(b, c))) / 2f;

    private static float T1(float b, float c) => (-b+Mathf.Sqrt(Determinant(b, c))) / 2f;

    private static float Determinant(float b, float c) => b * b - 4 * c;

    public SphereData CreateStructFromObject() {
        return new SphereData(Position, Color);
    }
}

[BurstCompile]
public struct SphereData : IShape {
    public Vector3 Position;
    public Matrix4x4 WorldToLocalMatrix;
    public readonly Color Color;
    
    private const float RADIUS = 1f;
    private const float AMBIENT = 0.3f;
    private const float DIFFUSE = 0.7f;
    private const float REFLECTION = 0.8f;
    private const float SHINY = 30f;

    public SphereData(Vector3 position, Color color) : this() {
        Position = position;
        Color = color;
        
        var gameObject = new GameObject();
        gameObject.transform.position = Position;
        var objectTransform = gameObject.transform;
        WorldToLocalMatrix = objectTransform.worldToLocalMatrix;
    }

    public float? Intersect(Ray ray) {
        ray = ray.TransformRay(WorldToLocalMatrix);
        
        var o = ray.origin;
        var d = ray.direction;

        var b = 2f * (o.x * d.x + o.y * d.y + o.z * d.z);

        var c = o.x * o.x + o.y * o.y + o.z * o.z - RADIUS * RADIUS;

        if (Determinant(b, c) < 0f) {
            return null;
        }

        if (Math.Abs(Determinant(b, c)) < float.Epsilon) {
            var intersect = T0(b, c);
            if (intersect < 0) {
                return null;
            }
            
            return intersect;
        }

        var tList = new NativeArray<float> (2, Allocator.Temp) {
            [0] = T0(b, c),
            [1] = T1(b, c)
        };
        
        if (tList[0] < 0f || tList[1] < 0f) {
            return null;
        }
        
        var retVal = tList[0] < tList[1] ? tList[0] : tList[1];
        
        tList.Dispose();
        
        return retVal;
    }

    public Color CalculateColor(NativeArray<SphereData> sceneSpheres, NativeArray<PlaneData> scenePlanes, Vector3 cameraPosition, Vector3 intersectPoint, NativeArray<LightData> lights) {
        var normal = (intersectPoint - Position).normalized;
        var retColor = new Color();

        for (var i = 0; i < lights.Length; i++) {
            var light = lights[i];
            var collision = false;
            var lightPosition = light.Position;
            for (var index = 0; index < sceneSpheres.Length; index++) {
                var s = sceneSpheres[index];
                if (s.Position == Position) continue;

                var intersectToLight = new Ray(intersectPoint, lightPosition - intersectPoint);
                var intersectAtLength = s.Intersect(intersectToLight) ?? float.MaxValue;
                var magnitudeLight = Vector3.Magnitude(lightPosition - intersectPoint);
                collision = intersectAtLength < magnitudeLight;
            }

            for (var index = 0; index < scenePlanes.Length; index++) {
                var p = scenePlanes[index];
                var intersectToLight = new Ray(intersectPoint, lightPosition - intersectPoint);
                var intersectAtLength = p.Intersect(intersectToLight) ?? float.MaxValue;
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
    
    private static float T0(float b, float c) => (-b-Mathf.Sqrt(Determinant(b, c))) / 2f;

    private static float T1(float b, float c) => (-b+Mathf.Sqrt(Determinant(b, c))) / 2f;

    private static float Determinant(float b, float c) => b * b - 4 * c;
}