using System;
using UnityEngine;

public abstract class Camera : SceneObject {
    private Vector3 lookAt;
    private Vector3 upVector;
    private float viewAngle;
    private float focalLength;

    public Vector3 CalculateDestinationPoint() {
        throw new NotImplementedException();
    }
}
