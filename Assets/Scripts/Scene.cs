using System;
using System.Collections.Generic;
using UnityEngine;

public class Scene {
    public List<Shape> shapeList = new List<Shape>();
    protected List<Light> lightList = new List<Light>();
    private Camera camera;

    public void CreateSphere(Vector3 position, Color color) {
        shapeList.Add(new Sphere(position, color));
    }

    public void CreatePlane(Vector3 position, Vector3 normal, Color color) {
        shapeList.Add(new Plane(position, normal, color));
        
    }

    public void CreatePointLight() {
        lightList.Add(new PointLight(Vector3.zero, Color.white));
    }

    public Camera CreatePerspCamera() {
        camera = new PerspCam(new Vector3(0,0,-17), Vector3.zero, Vector3.up, 35f,5,1920,1080);
        return camera;
    }

}
