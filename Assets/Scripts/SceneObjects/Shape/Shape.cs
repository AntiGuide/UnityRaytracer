using System.Collections.Generic;
using UnityEngine;

public abstract class Shape : SceneObject {
    public Material material;

    protected Shape(Vector3 position, Color color) : base(position, color) {
    }

    public abstract float? Intersect(Ray ray);
    
    public abstract Color CalculateColor(Scene scene, Vector3 intersectPoint, List<Light> list);
}
