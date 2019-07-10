using UnityEngine;

public abstract class Light : SceneObject {
    private Color color;

    protected Light(Vector3 position, Color color) : base(position) {
        this.color = color;
    }

    public Color GetColor => color;
}
