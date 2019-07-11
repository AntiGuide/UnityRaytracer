using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Material {
    protected Material(Shape parent) {
        this.parent = parent;
    }

    protected readonly Shape parent;

    protected Color GetColor => parent.color;

    public abstract Color CalculateColorSphere(Scene scene, Vector3 intersectPoint, List<Light> list);
}
