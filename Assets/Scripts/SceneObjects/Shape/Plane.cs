using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Plane : Shape {
    private readonly Vector3 normal;
    private float Q;

    public Plane(Vector3 position, Vector3 normal, Color color) : base(position, color) {
        this.normal = normal;
    }

    public override float? Intersect(Ray ray) {
        //ray = ray.TransformRay(worldToLocalMatrix);
        
        if (Math.Abs(Vector3.Dot(normal, ray.direction)) < float.Epsilon) return null;

        Q = Vector3.Dot(position, normal);
        var t = T(normal, ray.origin, Q ,ray.direction);

        if (t < 0) {
            return null;
        }
        
        return t;
    }

    public override Color CalculateColor(Scene scene, Vector3 intersectPoint, List<Light> list) {
        return color;
    }

    private static float T(Vector3 Pn, Vector3 P0, float Q, Vector3 D) => (Vector3.Dot(Pn,P0) + Q) / Vector3.Dot(Pn,D);
}
