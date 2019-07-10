using UnityEngine;

public class Plane : Shape {
    public Plane(Vector3 position) : base(position) {
    }

    public override float? Intersect(Ray ray) {
        throw new System.NotImplementedException();
    }
}
