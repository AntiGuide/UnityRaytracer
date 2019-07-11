using System.Collections.Generic;
using UnityEngine;

public class Blinn : Material {
    public Blinn(Shape parent) : base(parent) {
    }

    public override Color CalculateColorSphere(Scene scene, Vector3 intersectPoint, List<Light> list) {
        throw new System.NotImplementedException();
    }
}