using System.Collections.Generic;
using UnityEngine;

public class Plane : Shape {
    private readonly Vector3 normal;
    private float Q;

    public Plane(Vector3 position, Vector3 normal, Color color) : base(position, color) {
        this.normal = normal;
        this.material = new Phong(this);
    }
    
    public Plane(Vector3 position, Vector3 normal, Color color, float reflection, float shiny) : base(position, color) {
        this.normal = normal;
        this.material = new Phong(this, reflection, shiny);
    }

    protected enum Material {
        LAMBERT,
    }

    protected Plane(Vector3 position, Vector3 normal, Color color, Material material) : base(position, color) {
        this.normal = normal;
        this.material = new Lambert(this);
    }

    public override float? Intersect(Ray ray) {
        var denominator = Vector3.Dot(-normal, ray.direction);
        if (!(denominator > 0.000001f)) return null;
        
        var p0l0 = position - ray.origin; 
        var t = Vector3.Dot(p0l0, -normal) / denominator;
        if (t >= 0f) return t;

        return null; 
    }

    public override Color CalculateColor(Scene scene, Vector3 intersectPoint, List<Light> list) {
        return material.CalculateColorPlane(scene, intersectPoint, list, normal);
    }
}