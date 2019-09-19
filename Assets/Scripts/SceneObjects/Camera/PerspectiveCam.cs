using UnityEngine;

public class PerspectiveCam : Camera {
    public PerspectiveCam(Vector3 viewpoint, Vector3 centerOfInterest, Vector3 userUp, float viewAngle, float focalLength, int xMax, int yMax) : base(viewpoint, centerOfInterest, userUp, viewAngle, focalLength, xMax, yMax, Color.black) {
    }
}
