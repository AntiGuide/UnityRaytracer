using UnityEngine;

public abstract class Camera : SceneObject {
    public readonly int xMax;
    public readonly int yMax;
    
    private readonly Vector3 lookAt;
    private readonly Vector3 upVector;
    private readonly Vector3 sideVector;
    private readonly float h;
    private readonly float w;

    protected Camera(Vector3 viewpoint, Vector3 centerOfInterest, Vector3 UserUp, float viewAngle/*, float focalLength*/, int xMax, int yMax) : base(viewpoint, Color.black){
        lookAt = (centerOfInterest - viewpoint).normalized;
        sideVector = Vector3.Cross(lookAt, UserUp).normalized;
        upVector = Vector3.Cross(sideVector, lookAt).normalized;
        var aspectRatio = (xMax - 1) / (float)(yMax - 1);
        this.xMax = xMax;
        this.yMax = yMax;
        h = 2 * Mathf.Tan(viewAngle * Mathf.Deg2Rad /2f);
        w = aspectRatio * h;
    }

    /// <summary>x=0 y=0 Is bottom left</summary>
    public Vector3 CalculateDestinationPoint(int x, int y) {
        var normalizedPos = new Vector2 {
            x = 2f * ((x + 0.5f) / xMax) - 1f,
            y = 2f * ((y + 0.5f) / yMax) - 1f
        };

        var p1 = h * normalizedPos.y * upVector + w * normalizedPos.x * sideVector;
        return (p1 + lookAt).normalized;
    }
}
