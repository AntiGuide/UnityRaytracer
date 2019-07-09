using System.Collections.Generic;

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

    public void CreatePerspCamera() {
        camera = new PerspCam();
    }

}
