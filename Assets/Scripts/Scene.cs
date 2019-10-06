using System;
using System.Collections.Generic;
using UnityEngine;

public class Scene {
    public readonly List<Shape> shapeList = new List<Shape>();
    public readonly List<Light> lightList = new List<Light>();
    public Camera Camera;

    //public static UnityEngine.Plane ScenePlane;

    public void CreateSphere(Vector3 position, Color color) {
        shapeList.Add(new Sphere(position, color));
    }

    public void CreatePlane(Vector3 position, Vector3 normal, Color color) {
        shapeList.Add(new Plane(position, normal, color));
//        var go = GameObject.CreatePrimitive(PrimitiveType.Plane);
//        go.transform.position = position;
//        go.transform.LookAt(position + normal);
//        //go.transform.Rotate(new Vector3 (90f,0f,0f));
    }

    public void CreatePointLight(Vector3 position) {
        lightList.Add(new PointLight(position, Color.white, 1f));
    }

    public Camera CreatePerspCamera(Vector3 viewpoint, Vector3 centerOfInterest, Vector3 userUp, float viewAngle, int xMax, int yMax) {
        Camera = new PerspCam(viewpoint, centerOfInterest, userUp, viewAngle, xMax, yMax);
        return Camera;
    }

}
