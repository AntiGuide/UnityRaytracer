using System;
using System.Collections.Generic;
using UnityEngine;

public class Scene {
    public List<Shape> shapeList = new List<Shape>();
    protected List<Light> lightList = new List<Light>();
    private Camera camera;

    public void CreateSphere() {
        shapeList.Add(new Sphere(Vector3.zero));
    }

    public void CreatePlane() {
        shapeList.Add(new Plane(Vector3.zero));
        
    }

    public void CreatePointLight() {
        lightList.Add(new PointLight(Vector3.zero, Color.white));
    }

    public Camera CreatePerspCamera() {
        //camera = new PerspCam(Vector3.zero, Vector3.forward, Vector3.up, 90f,5,1920,1080);
        camera = new PerspCam(new Vector3(0,0,-17), Vector3.zero, Vector3.up, 35f,5,1920,1080);
        return camera;
    }

}
