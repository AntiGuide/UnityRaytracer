using UnityEngine;

public class PerspCam : Camera {
    public PerspCam(Vector3 viewpoint, Vector3 centerOfInterest, Vector3 UserUp, float viewAngle, float focalLength, int xMax, int yMax) : base(viewpoint, centerOfInterest, UserUp, viewAngle, focalLength, xMax, yMax, Color.black) {
    }
}
