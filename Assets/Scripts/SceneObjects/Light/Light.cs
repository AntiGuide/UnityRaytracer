using UnityEngine;

public abstract class Light : SceneObject {

    public readonly float Intensity;
    
    protected Light(Vector3 position, Color color, float intensity) : base(position, color) {
        this.Intensity = intensity;
    }
}
