using System;
using UnityEngine;

public abstract class SceneObject : IEquatable<SceneObject>{
    public Vector3 Position;

    protected Matrix4x4 WorldToLocalMatrix;
    public readonly Color Color;

    protected SceneObject(Vector3 position, Color color) {
        Position = position;
        Color = color;
        
        var gameObject = new GameObject();
        gameObject.transform.position = Position;
        var objectTransform = gameObject.transform;
        WorldToLocalMatrix = objectTransform.worldToLocalMatrix;
    }

    public void UpdatePosition(Vector3 position) {
        this.Position = position;
        
        var gameObject = new GameObject();
        gameObject.transform.position = this.Position;
        var objectTransform = gameObject.transform;
        WorldToLocalMatrix = objectTransform.worldToLocalMatrix;
    }

    public bool Equals(SceneObject other) {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Position.Equals(other.Position) && Color.Equals(other.Color);
    }

    public override bool Equals(object obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((SceneObject) obj);
    }

    public override int GetHashCode() {
        unchecked {
            return (Position.GetHashCode() * 397) ^ Color.GetHashCode();
        }
    }
}