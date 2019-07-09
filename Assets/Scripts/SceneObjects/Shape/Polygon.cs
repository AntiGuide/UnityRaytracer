using System;
using System.Collections.Generic;
using System.Linq;

public class Polygon : Shape {
    private List<Triangle> triangles;

    public Polygon(List<Triangle> triangles) {
        if (triangles.Count == 0) throw new ArgumentException("Triangle count of polygon may only be greater than 0");
        
        var tmpTriangles = new Triangle[triangles.Count];
        triangles.CopyTo(tmpTriangles);
        this.triangles = tmpTriangles.ToList();
    }
}
