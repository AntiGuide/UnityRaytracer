using UnityEngine;

public class Main : MonoBehaviour {
    private Raytracer raytracer;
    private Scene scene;
    
    [SerializeField]
    private RenderWindow renderWindow;
    
    private Camera mainCamera;
    
    private void Start() {
        Screen.SetResolution(renderWindow.ResolutionWidth, renderWindow.ResolutionHeight,FullScreenMode.Windowed);
        
        SetupScene();
        SetupLights();
        SetupCameras();
        SetupCornellBox();
        
        raytracer = new Raytracer(scene, Color.black, mainCamera);
        RaytraceScene();
    }

    private void SetupScene() {
        scene = new Scene();
    }

    private void SetupLights() {
        scene.CreatePointLight(new Vector3(0f,2.9f,10f));
    }

    private void SetupCameras() {
        mainCamera = scene.CreatePerspectiveCamera(new Vector3(0,0,17), Vector3.zero, Vector3.up, 35f, renderWindow.ResolutionWidth, renderWindow.ResolutionHeight);
    }

    private void SetupCornellBox() {
        scene.CreateSphere(new Vector3(1f,-2f,9f), Color.red);
        scene.CreateSphere(new Vector3(-1f,-2f,11f), Color.green);
        
        scene.CreateSquare(new Vector3(0f,2.999f,10f), Vector3.down, Color.white, 1f);
        
        scene.CreatePlane(new Vector3(-4f, 0, 15.5f), Vector3.right,   Color.red,   0f, 0f); //Right
        scene.CreatePlane(new Vector3( 4f, 0, 15.5f), Vector3.left,    Color.blue,  0f, 0f); //Left
        scene.CreatePlane(new Vector3( 0f, 3f,15.5f), Vector3.down,    Color.white, 0f, 0f); //Top
        scene.CreatePlane(new Vector3( 0f,-3f,15.5f), Vector3.up,      Color.white, 0f, 0f); //Bottom
        scene.CreatePlane(new Vector3( 0f, 0f,   8f), Vector3.forward, Color.white, 0f, 0f); //Back
        scene.CreatePlane(new Vector3( 0f, 0f,  20f), Vector3.back,    Color.white, 0f, 0f); //BehindCam
    }

    private void RaytraceScene() {
        renderWindow.SetPixels(raytracer.Render());
    }

}
