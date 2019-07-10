using UnityEngine;

public class OrthoCam : Camera {
    public OrthoCam(Vector3 viewpoint, Vector3 centerOfInterest, Vector3 UserUp, float viewAngle, float focalLength, int xMax, int yMax, Color color) : base(viewpoint, centerOfInterest, UserUp, viewAngle, focalLength, xMax, yMax, color) {
    }
}
