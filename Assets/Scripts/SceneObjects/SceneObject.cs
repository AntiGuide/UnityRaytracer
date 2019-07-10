using UnityEngine;

public abstract class SceneObject {
    public Vector3 position;

    protected SceneObject(Vector3 position) {
        this.position = position;
    }
}
