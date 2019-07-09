using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class RenderWindow : SerializedMonoBehaviour {

    [SerializeField] private RawImage rawImage;

    [SerializeField] private int resolutionWidth;
    
    [SerializeField] private int resolutionHeight;
    
    private Texture2D rawImageTexture;

    private void Start() {
        rawImageTexture = new Texture2D(resolutionWidth, resolutionHeight);
        SetFillColor(Color.grey);
    }

    private void SetFillColor(Color color) {
        var colors = new Color[rawImageTexture.width * rawImageTexture.height];
        
        for (var index = 0; index < colors.Length; index++) {
            colors[index] = color;
        }

        rawImageTexture.SetPixels(colors);
        rawImageTexture.Apply();
        rawImage.texture = rawImageTexture;
    }

    public void SetPixel(int x, int y, Color color) {
        rawImageTexture.SetPixel(x, y, color);
    }
}
