using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shape : SceneObject, IEquatable<Shape> {
    public Material Material;

    protected Shape(Vector3 position, Color color) : base(position, color) {
    }

    public abstract float? Intersect(Ray ray);
    
    public abstract Color CalculateColor(Scene scene, Vector3 intersectPoint, List<Light> list);

    public bool Equals(Shape other) {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Equals(Material, other.Material);
    }

    public override bool Equals(object obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((Shape) obj);
    }

    public override int GetHashCode() {
        unchecked {
            return (base.GetHashCode() * 397) ^ (Material != null ? Material.GetHashCode() : 0);
        }
    }
}
