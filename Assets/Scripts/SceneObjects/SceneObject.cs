using System;
using UnityEngine;

public abstract class SceneObject : IEquatable<SceneObject>{
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

    public bool Equals(SceneObject other) {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return position.Equals(other.position) && color.Equals(other.color);
    }

    public override bool Equals(object obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SceneObject) obj);
    }

    public override int GetHashCode() {
        unchecked {
            return (position.GetHashCode() * 397) ^ color.GetHashCode();
        }
    }
}
