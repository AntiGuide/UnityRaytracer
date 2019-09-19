using System.Collections.Generic;
using UnityEngine;

public abstract class Material {
    protected Material(Shape parent) {
        this.Parent = parent;
    }

    protected readonly Shape Parent;

    protected Color GetColor => Parent.Color;

    public abstract Color CalculateColorSphere(Scene scene, Vector3 intersectPoint, List<Light> list);

    public abstract Color CalculateColorPlane(Scene scene, Vector3 intersectPoint, List<Light> list, Vector3 normal);
}