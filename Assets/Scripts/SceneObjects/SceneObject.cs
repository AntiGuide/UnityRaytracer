using System;
using UnityEngine;

public abstract class SceneObject : IEquatable<SceneObject>{
    public Vector3 position;

    protected Matrix4x4 worldToLocalMatrix;
    public Color color;

    protected SceneObject(Vector3 position, Color color) {
        this.position = position;
        this.color = color;
        worldToLocalMatrix = new Matrix4x4();
        worldToLocalMatrix.SetTRS(position, Quaternion.identity, Vector3.one);
        worldToLocalMatrix = worldToLocalMatrix.inverse;
    }

    public void UpdatePosition(Vector3 position) {
        this.position = position;
        worldToLocalMatrix.SetTRS(position, Quaternion.identity, Vector3.one);
        worldToLocalMatrix = worldToLocalMatrix.inverse;
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
