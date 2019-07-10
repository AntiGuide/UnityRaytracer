using UnityEngine;

public abstract class Shape : SceneObject {
    protected Shape(Vector3 position, Color color) : base(position, color) {
    }

    public abstract float? Intersect(Ray ray);
}
