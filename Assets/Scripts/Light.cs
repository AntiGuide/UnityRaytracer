using UnityEngine;

public abstract class Light : SceneObject {
    private Color color;
    
    public Color GetColor => color;
}
