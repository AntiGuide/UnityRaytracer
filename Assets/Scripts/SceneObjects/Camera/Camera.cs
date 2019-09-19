using UnityEngine;
using UnityEngine.UIElements;

public abstract class Camera : SceneObject {
    
    /// <summary>ViewVector</summary>
    private readonly Vector3 lookAt;
    
    /// <summary>CameraUpVector</summary>
    private readonly Vector3 upVector;
    
    private float viewAngle;
    private float focalLength;

    private readonly Vector3 sideVector;
    private Vector3 windowCenter;

    public readonly int XMax;
    public readonly int YMax;
    
    private readonly float h;
    private readonly float w;

    protected Camera(Vector3 viewpoint, Vector3 centerOfInterest, Vector3 userUp, float viewAngle, float focalLength, int xMax, int yMax, Color color) : base(viewpoint, color){
        lookAt = (centerOfInterest - viewpoint).normalized;
        sideVector = Vector3.Cross(lookAt, userUp).normalized;
        upVector = Vector3.Cross(sideVector, lookAt).normalized;

        this.viewAngle = viewAngle;
        this.focalLength = focalLength;
        
        var aspectRatio = xMax / (float)yMax;

        XMax = --xMax;
        YMax = --yMax;
        
        h = 2 * Mathf.Tan(viewAngle * Mathf.Deg2Rad /2f);
        w = aspectRatio * h;

        windowCenter = Position + lookAt;

        //this.centerPoint = position;
    }

    public Vector3 CalculateDestinationPoint(int x, int y) {
        var normalizedPos = new Vector2 {
            x = 2f * ((x + 0.5f) / (XMax + 1)) - 1f,
            y = 2f * ((y + 0.5f) / (YMax + 1)) - 1f
        };

        var p1 = h * normalizedPos.y * upVector + w * normalizedPos.x * sideVector;
        return (p1 + lookAt).normalized;
    }

    public CameraData CreateStructFromObject() {
        return new CameraData(
            lookAt,
            upVector,
            viewAngle,
            focalLength,
            sideVector,
            windowCenter,
            XMax,
            YMax,
            h,
            w,
            Position
        );
    }
}

public struct CameraData {
    public Vector3 LookAt;
    public Vector3 UpVector;

    public float ViewAngle;
    public float FocalLength;

    public Vector3 SideVector;
    public Vector3 WindowCenter;

    public int XMax;
    public int YMax;

    public float H;
    public float W;
    
    public Vector3 Position;

    public CameraData(Vector3 lookAt, Vector3 upVector, float viewAngle, float focalLength, Vector3 sideVector, Vector3 windowCenter, int xMax, int yMax, float h, float w, Vector3 position) {
        LookAt = lookAt;
        UpVector = upVector;
        ViewAngle = viewAngle;
        FocalLength = focalLength;
        SideVector = sideVector;
        WindowCenter = windowCenter;
        XMax = xMax;
        YMax = yMax;
        H = h;
        W = w;
        Position = position;
    }
}