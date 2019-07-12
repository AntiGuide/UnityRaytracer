using System;
using System.Collections.Generic;
using UnityEngine;

public class Scene {
    public readonly List<Shape> shapeList = new List<Shape>();
    public readonly List<Light> lightList = new List<Light>();
    public Camera Camera;

    public void CreateSphere(Vector3 position, Color color) {
        shapeList.Add(new Sphere(position, color));
    }

    public void CreatePlane(Vector3 position, Vector3 normal, Color color) {
        shapeList.Add(new Plane(position, normal, color));
        
    }

    public void CreatePointLight(Vector3 position) {
        lightList.Add(new PointLight(position, Color.white, 1f));
    }

    public Camera CreatePerspCamera() {
        Camera = new PerspCam(new Vector3(0,0,-17), Vector3.zero, Vector3.up, 35f,5,RenderWindow.Instance.ResolutionWidth,RenderWindow.Instance.ResolutionHeight);
        return Camera;
    }

}
