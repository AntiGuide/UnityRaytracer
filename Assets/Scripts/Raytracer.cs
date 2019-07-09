using System;
using System.Collections.Generic;
using UnityEngine;

public class Raytracer {
    private List<Shape> shapeList;
    private List<Light> lightList;
    private Scene scene;
    private RenderWindow renderWindow;
    private int maxRecursions;
    private Color backgroundColor;

    public void Render() {
        throw new NotImplementedException();
    }

    public Color SendPrimaryRay() {
        throw new NotImplementedException();
    }

    public Color TraceRay() {
        throw new NotImplementedException();
    }

    public Color Shade() {
        throw new NotImplementedException();
    }

    public Color TraceIllumination() {
        throw new NotImplementedException();
    }
}