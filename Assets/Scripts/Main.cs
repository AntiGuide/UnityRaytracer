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
        scene.CreateSphere();
    }

    private void RaytraceScene() {
        renderWindow.SetPixels(raytracer.Render());
    }

}
