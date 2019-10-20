using UnityEngine;

public static class RayExtension  {
    public static Ray TransformRay(this Ray ray, Matrix4x4 worldToLocalMatrix) {
        ray.origin = worldToLocalMatrix.MultiplyPoint(ray.origin);
        return ray;
    }
}
