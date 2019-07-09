using UnityEngine;

public class Main : MonoBehaviour {
    private Raytracer raytracer;
    private Scene scene;
    
    private void Start() {
        raytracer = new Raytracer();
        
        SetupScene();
        SetupLights();
        SetupCameras();
        SetupCornellBox();
        
        RaytraceScene();
    }

    private void Update() {
        Draw();
    }

    private void Draw() {
        raytracer.Render();
    }

    private void SetupScene() {
        scene = new Scene();
    }

    private void SetupLights() {
        throw new System.NotImplementedException();
    }

    private void SetupCameras() {
        throw new System.NotImplementedException();
    }

    private void SetupCornellBox() {
        throw new System.NotImplementedException();
    }

    private void RaytraceScene() {
        throw new System.NotImplementedException();
    }

}
