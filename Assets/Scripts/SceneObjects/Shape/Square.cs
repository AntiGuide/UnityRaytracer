using UnityEngine;

public class Square : Plane {
    private readonly float size;
    private readonly float ySize;

    public Square(Vector3 position, Vector3 normal, Color color, float size) : base(position, normal, color, Material.LAMBERT) {
        this.size = size;
    }

    public override float? Intersect(Ray ray) {
        var t = base.Intersect(ray);
        if (t == null) return null;
        
        var pointOnPlane = ray.GetPoint(t.Value);
        if (size / 2f < Mathf.Abs(position.x - pointOnPlane.x)) return null;
        if (size / 2f < Mathf.Abs(position.y - pointOnPlane.y)) return null;
        if (size / 2f < Mathf.Abs(position.z - pointOnPlane.z)) return null;

        return t.Value;
    }
}