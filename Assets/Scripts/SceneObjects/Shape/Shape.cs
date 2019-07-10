using UnityEngine;

public abstract class Shape : SceneObject {
    protected Shape(Vector3 position) : base(position) {
    }

    public abstract float? Intersect(Ray ray);
}
