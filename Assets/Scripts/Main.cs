using UnityEngine;

public class Main : MonoBehaviour {
    private Raytracer raytracer;
    private Scene scene;
    
    [SerializeField]
    private RenderWindow renderWindow;
    
    private Camera myCamera;
    
    private void Start() {
        Screen.SetResolution(renderWindow.ResolutionWidth, renderWindow.ResolutionHeight,FullScreenMode.Windowed);
        
        SetupScene();
        SetupLights();
        SetupCameras();
        SetupCornellBox();
        
        raytracer = new Raytracer(scene, renderWindow, 1, Color.black, myCamera);
    }

    private void Update() {
        if (!Input.GetKeyDown(KeyCode.C)) return;
        
        raytracer.Render();
        RaytraceScene();

        //scene.ShapeList[0].UpdatePosition(scene.ShapeList[0].Position + Vector3.left * Input.GetAxis("Horizontal") + Vector3.down * Input.GetAxis("Vertical"));
    }

    private void SetupScene() {
        scene = new Scene();
    }

    private void SetupLights() {
        scene.CreatePointLight(new Vector3(-2f,-2f,-12f));
        //scene.CreatePointLight(new Vector3(2f,1f,-15f));
    }

    private void SetupCameras() {
        myCamera = scene.CreatePerspectiveCamera();
    }

    private void SetupCornellBox() {
        scene.CreateSphere(new Vector3(0f,0f,-10f), Color.green);
        
        scene.CreatePlane(new Vector3(0f,0f,25f), Vector3.back, Color.white);    //Back
        scene.CreatePlane(new Vector3(-3,0,0), Vector3.right, Color.blue);          //Right
        scene.CreatePlane(new Vector3(3,0,0), Vector3.left, Color.red);             //Left
        scene.CreatePlane(new Vector3(0,3,0), Vector3.down, Color.white);           //Bottom
        scene.CreatePlane(new Vector3(0,-3,0), Vector3.up, Color.white);            //Top
        scene.CreatePlane(new Vector3(0,0,16), Vector3.back, Color.white);          //Front
    }

    private void RaytraceScene() {
        renderWindow.SetPixels(raytracer.RenderComplete());
    }

}
