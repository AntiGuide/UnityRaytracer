using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sphere : Shape {
    private const float radius = 1f;

    public Sphere(Vector3 position, Color color) : base(position, color) {
        this.material = new Phong(this);
    }

    public override float? Intersect(Ray ray) {
        ray = ray.TransformRay(worldToLocalMatrix);
        var o = ray.origin;
        var d = ray.direction;
        var B = 2f * (o.x * d.x + o.y * d.y + o.z * d.z);
        var C = o.x * o.x + o.y * o.y + o.z * o.z - radius * radius;
        if (Determinant(B, C) < 0f) return null;

        if (Math.Abs(Determinant(B, C)) < float.Epsilon) {
            var intersect = T0(B, C);
            if (intersect < 0) return null;

            return intersect;
        }

        var tList = new List<float>(2) {
            T0(B, C),
            T1(B, C)
        };
        
        tList.RemoveAll(f => f < 0f);
        if (tList.Count == 0) return null;

        return tList.Min();
    }

    public override Color CalculateColor(Scene scene, Vector3 intersectPoint, List<Light> list) {
        return material.CalculateColorSphere(scene, intersectPoint, list);
    }

    private static float T0(float B, float C) => (-B-Mathf.Sqrt(Determinant(B, C))) / 2f;

    private static float T1(float B, float C) => (-B+Mathf.Sqrt(Determinant(B, C))) / 2f;

    private static float Determinant(float B, float C) => B * B - 4 * C;
}