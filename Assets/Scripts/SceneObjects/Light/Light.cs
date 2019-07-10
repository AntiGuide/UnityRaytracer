using UnityEngine;

public abstract class Light : SceneObject {

    protected Light(Vector3 position, Color color) : base(position, color) {
        
    }

    public Color GetColor => color;
}
