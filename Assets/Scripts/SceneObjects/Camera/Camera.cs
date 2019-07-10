using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Camera : SceneObject {
    
    /// <summary>ViewVector</summary>
    private readonly Vector3 lookAt;
    
    /// <summary>CameraUpVector</summary>
    private readonly Vector3 upVector;
    
    private float viewAngle;
    private float focalLength;

    private readonly Vector3 sideVector;
    private Vector3 windowCenter;
    
    private readonly int xMax;
    private readonly int yMax;
    
    private readonly float h;
    private readonly float w;

    protected Camera(Vector3 viewpoint, Vector3 centerOfInterest, Vector3 UserUp, float viewAngle, float focalLength, int xMax, int yMax) {
        position = viewpoint;

        lookAt = (centerOfInterest - viewpoint).normalized;
        sideVector = Vector3.Cross(lookAt, UserUp).normalized;
        upVector = Vector3.Cross(sideVector, lookAt).normalized;

        this.viewAngle = viewAngle;
        this.focalLength = focalLength;

        this.xMax = --xMax;
        this.yMax = --yMax;

        var aspectRatio = xMax / yMax;
        h = 2 * Mathf.Tan(viewAngle/2f);
        w = aspectRatio * h;

        windowCenter = position + lookAt;

        //this.centerPoint = position;
    }

    private Vector3 CalculateDestinationPoint(int x, int y) {
        var normalizedPos = new Vector2 {
            x = 2f * ((x + 0.5f) / xMax) - 1f,
            y = 2f * ((y + 0.5f) / yMax) - 1f
        };

        var p1 = h * normalizedPos.y * upVector + w * normalizedPos.x * sideVector;
        
        return (p1 + lookAt).normalized;
    }

    public Color[] Render() {
        var ret = new Color[(xMax + 1) * (yMax + 1)];
        var dt1 = DateTime.Now;

        Parallel.For(0,yMax, y => {
            Parallel.For(0, xMax, (x) => {
                var tmpVec = CalculateDestinationPoint(x, y);
                var color = new Color((tmpVec.x + 1f) / 2f, (tmpVec.y + 1f) / 2f, (tmpVec.z + 1f) / 2f);
                ret[(yMax - y) * (xMax + 1) + x] = color;
            });
        });

        var dt2 = DateTime.Now;
        Debug.Log((dt2 - dt1).Milliseconds);
        return ret;
    }
}
