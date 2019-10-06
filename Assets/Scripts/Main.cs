using UnityEngine;

public class Main : MonoBehaviour {
    private Raytracer raytracer;
    private Scene scene;
    
    [SerializeField]
    private RenderWindow renderWindow;
    
    private Camera camera;
    
    private void Start() {
        Screen.SetResolution(renderWindow.ResolutionWidth, renderWindow.ResolutionHeight,FullScreenMode.Windowed);
        
        SetupScene();
        SetupLights();
        SetupCameras();
        SetupCornellBox();
        
        raytracer = new Raytracer(scene, renderWindow, 1, Color.black, camera);
        RaytraceScene();
    }

    private void Update() {
        //RaytraceScene();
        //scene.shapeList[0].UpdatePosition(scene.shapeList[0].position + Vector3.left * Input.GetAxis("Horizontal") + Vector3.down * Input.GetAxis("Vertical"));
    }

    private void SetupScene() {
        scene = new Scene();
    }

    private void SetupLights() {
        scene.CreatePointLight(new Vector3(0f,2.9f,10f));
    }

    private void SetupCameras() {
        camera = scene.CreatePerspCamera(new Vector3(0,0,17), Vector3.zero, Vector3.up, 35f, renderWindow.ResolutionWidth, renderWindow.ResolutionHeight);
    }

    private void SetupCornellBox() {
        scene.CreateSphere(new Vector3(0f,0f,10f), Color.green);
        
        scene.CreatePlane(new Vector3(-4f,0,15.5f), Vector3.right, Color.red);          //Right
        scene.CreatePlane(new Vector3(4f,0,15.5f), Vector3.left, Color.blue);          //Left
        scene.CreatePlane(new Vector3(0f,3f,15.5f), Vector3.down, Color.white);          //Top
        scene.CreatePlane(new Vector3(0f,-3f,15.5f), Vector3.up, Color.white);          //Bottom
        scene.CreatePlane(new Vector3(0f,0f,8f), Vector3.forward, Color.white);          //Back
        scene.CreatePlane(new Vector3(0f,0f,20f), Vector3.back, Color.white);          //BehindCam
    }

    private void RaytraceScene() {
        renderWindow.SetPixels(raytracer.Render());
    }

}
