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
        scene.shapeList[0].UpdatePosition(scene.shapeList[0].position + Vector3.left * Input.GetAxis("Horizontal") + Vector3.down * Input.GetAxis("Vertical"));
    }

    private void SetupScene() {
        scene = new Scene();
    }

    private void SetupLights() {
        scene.CreatePointLight(new Vector3(-4f,0f,-10f));
        //scene.CreatePointLight(new Vector3(2f,1f,-15f));
    }

    private void SetupCameras() {
        camera = scene.CreatePerspCamera();
    }

    private void SetupCornellBox() {
        scene.CreateSphere(new Vector3(0f,0f,-10f), Color.green);
        
        scene.CreatePlane(new Vector3(0f,0f,25f), Vector3.forward, Color.white);    //Back
        scene.CreatePlane(new Vector3(-3,0,0), Vector3.right, Color.blue);          //Right
        scene.CreatePlane(new Vector3(3,0,0), Vector3.left, Color.red);             //Left
        scene.CreatePlane(new Vector3(0,3,0), Vector3.down, Color.white);           //Bottom
        scene.CreatePlane(new Vector3(0,-3,0), Vector3.up, Color.white);            //Top
        scene.CreatePlane(new Vector3(0,0,16), Vector3.back, Color.white);          //Front
    }

    private void RaytraceScene() {
        renderWindow.SetPixels(raytracer.Render());
    }

}
