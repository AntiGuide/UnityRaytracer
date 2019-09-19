using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Polygon : Shape {
    private List<Triangle> triangles;

    public Polygon(Vector3 position, List<Triangle> triangles, Color color) : base(position, color) {
        if (triangles.Count == 0) throw new ArgumentException("Triangle count of polygon may only be greater than 0");
        
        var tmpTriangles = new Triangle[triangles.Count];
        triangles.CopyTo(tmpTriangles);
        this.triangles = tmpTriangles.ToList();
    }

    public override float? Intersect(Ray ray) {
        throw new NotImplementedException();
    }

    public override Color CalculateColor(Scene scene, Vector3 intersectPoint, List<Light> list) {
        return Color;
    }
}
