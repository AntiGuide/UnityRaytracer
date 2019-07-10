using UnityEngine;

public class Triangle : Shape {
    public Triangle(Vector3 position, Color color) : base(position, color) {
    }

    public override float? Intersect(Ray ray) {
        throw new System.NotImplementedException();
    }
}
