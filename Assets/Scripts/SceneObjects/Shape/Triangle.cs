using UnityEngine;

public class Triangle : Shape {
    public Triangle(Vector3 position) : base(position) {
    }

    public override float? Intersect(Ray ray) {
        throw new System.NotImplementedException();
    }
}
