using UnityEngine;

public class Main : MonoBehaviour {
    private Raytracer raytracer;
    private Scene scene;
    
    [SerializeField]
    private RenderWindow renderWindow;
    
    private Camera camera;
    
    private void Start() {
        SetupScene();
        //SetupLights();
        SetupCameras();
        SetupCornellBox();
        
        raytracer = new Raytracer(scene, renderWindow, 1, Color.black, camera);
        RaytraceScene();
    }

    private void Draw() {
        
    }

    private void SetupScene() {
        scene = new Scene();
    }

    private void SetupLights() {
        throw new System.NotImplementedException();
    }

    private void SetupCameras() {
        camera = scene.CreatePerspCamera();
    }

    private void SetupCornellBox() {
        scene.CreateSphere(new Vector3(0f,0f,0f), Color.green);
        //scene.CreatePlane(new Vector3(0,0,-6), Vector3.forward, Color.green);    //Front
        scene.CreatePlane(new Vector3(-4,0,0), Vector3.right, Color.blue);    //Right
        scene.CreatePlane(new Vector3(4,0,0), Vector3.left, Color.magenta);     //Left
        scene.CreatePlane(new Vector3(0,3,0), Vector3.down, Color.red);     //Bottom
        scene.CreatePlane(new Vector3(0,-3,0), Vector3.up, Color.yellow);    //Top
        //scene.CreatePlane(new Vector3(0,0,18), Vector3.back, Color.gray);    //Back
    }

    private void RaytraceScene() {
        renderWindow.SetPixels(raytracer.Render());
    }

}
