using System;
using System.Collections.Generic;
using UnityEngine;

public class Scene {
    private List<Shape> shapeList = new List<Shape>();
    private List<Light> lightList = new List<Light>();
    private Camera camera;

    public void CreateSphere() {
        shapeList.Add(new Sphere());
    }

    public void CreatePlane() {
        shapeList.Add(new Plane());
        
    }

    public void CreatePointLight() {
        lightList.Add(new PointLight());
    }

    public Camera CreatePerspCamera() {
        camera = new PerspCam(Vector3.zero, Vector3.forward, Vector3.up, 90f,5,1920,1080);
        return camera;
    }

}
