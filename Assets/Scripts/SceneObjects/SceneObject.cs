using UnityEngine;

public abstract class SceneObject {
    public Vector3 position;

    public Matrix4x4 worldToLocalMatrix;
    public Color color;

    protected SceneObject(Vector3 position, Color color) {
        this.position = position;
        this.color = color;
        
        var gameObject = new GameObject();
        gameObject.transform.position = this.position;
        var objectTransform = gameObject.transform;
        worldToLocalMatrix = objectTransform.worldToLocalMatrix;
    }

    public void UpdatePosition(Vector3 position) {
        this.position = position;
        
        var gameObject = new GameObject();
        gameObject.transform.position = this.position;
        var objectTransform = gameObject.transform;
        worldToLocalMatrix = objectTransform.worldToLocalMatrix;
    }
}
