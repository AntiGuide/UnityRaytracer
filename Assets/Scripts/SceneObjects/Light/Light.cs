using Unity.Burst;
using UnityEngine;

public abstract class Light : SceneObject {

    public float Intensity;
    
    protected Light(Vector3 position, Color color, float intensity) : base(position, color) {
        this.Intensity = intensity;
    }

    public Color GetColor => Color;
    
    public LightData CreateStructFromObject() {
        return new LightData(Position, Color, Intensity);
    }
}

[BurstCompile]
public struct LightData {
    public Vector3 Position;
    public readonly Color Color;
    public float Intensity;

    public LightData(Vector3 position, Color color, float intensity) : this() {
        Position = position;
        Color = color;
        Intensity = intensity;
    }

    public Color GetColor => Color;
}