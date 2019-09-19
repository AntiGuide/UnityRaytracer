using UnityEngine;

public interface IShape {
    float? Intersect(Ray r);
}