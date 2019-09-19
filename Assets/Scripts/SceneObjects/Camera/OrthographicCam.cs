using UnityEngine;

public class OrthographicCam : Camera {
    public OrthographicCam(Vector3 viewpoint, Vector3 centerOfInterest, Vector3 userUp, float viewAngle, float focalLength, int xMax, int yMax, Color color) : base(viewpoint, centerOfInterest, userUp, viewAngle, focalLength, xMax, yMax, color) {
    }
}
