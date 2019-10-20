using System.Collections.Generic;
using UnityEngine;

public class Scene {
    public readonly List<Shape> shapeList = new List<Shape>();
    public readonly List<Light> lightList = new List<Light>();
    
    public Camera Camera;

    public void CreateSphere(Vector3 position, Color color) {
        shapeList.Add(new Sphere(position, color));
    }

    public void CreateSquare(Vector3 position, Vector3 normal, Color color, float size) {
        shapeList.Add(new Square(position, normal, color, size));
    }

    public void CreatePlane(Vector3 position, Vector3 normal, Color color) {
        shapeList.Add(new Plane(position, normal, color));
    }

    public void CreatePlane(Vector3 position, Vector3 normal, Color color, float reflection, float shiny) {
        shapeList.Add(new Plane(position, normal, color, reflection, shiny));
    }

    public void CreatePointLight(Vector3 position) {
        lightList.Add(new PointLight(position, Color.white, 1f));
    }

    public Camera CreatePerspectiveCamera(Vector3 viewpoint, Vector3 centerOfInterest, Vector3 userUp, float viewAngle, int xMax, int yMax) {
        Camera = new PerspCam(viewpoint, centerOfInterest, userUp, viewAngle, xMax, yMax);
        return Camera;
    }

}
