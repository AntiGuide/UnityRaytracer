using System.Collections.Generic;
using UnityEngine;

public class Scene {
    public readonly List<Shape> ShapeList = new List<Shape>();
    public readonly List<Light> LightList = new List<Light>();
    
    public readonly List<Sphere> SphereList = new List<Sphere>();
    public readonly List<Plane> PlaneList = new List<Plane>();
    
    public Camera Camera;

    public void CreateSphere(Vector3 position, Color color) {
        var sphere = new Sphere(position, color);
        ShapeList.Add(sphere);
        SphereList.Add(sphere);
    }

    public void CreatePlane(Vector3 position, Vector3 normal, Color color) {
        var plane = new Plane(position, normal, color);
        ShapeList.Add(plane);
        PlaneList.Add(plane);

    }

    public void CreatePointLight(Vector3 position) {
        LightList.Add(new PointLight(position, Color.white, 1f));
    }

    public Camera CreatePerspectiveCamera() {
        Camera = new PerspectiveCam(new Vector3(0,0,-17), Vector3.zero, Vector3.up, 35f,5,RenderWindow.Instance.ResolutionWidth,RenderWindow.Instance.ResolutionHeight);
        return Camera;
    }

}
