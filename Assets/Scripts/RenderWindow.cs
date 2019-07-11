using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class RenderWindow : SerializedMonoBehaviour {

    public static RenderWindow Instance;
    
    [SerializeField] private RawImage rawImage;

    [SerializeField] private int resolutionWidth;
    
    [SerializeField] private int resolutionHeight;
    
    private Texture2D rawImageTexture;

    public int ResolutionWidth => resolutionWidth;

    public int ResolutionHeight => resolutionHeight;

    private void Awake() {
        if (Instance != null) {
            Application.Quit();
        }

        Instance = this;
        
        if (rawImageTexture == null) {
            rawImageTexture = new Texture2D(resolutionWidth, resolutionHeight);
        }
        //SetFillColor(Color.grey);
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
        rawImageTexture.Apply();
        rawImage.texture = rawImageTexture;
    }

    public void SetPixels(Color[] colors) {
        if (rawImageTexture == null) {
            rawImageTexture = new Texture2D(resolutionWidth, resolutionHeight);
        }
        
        rawImageTexture.SetPixels(colors);
        rawImageTexture.Apply();
        rawImage.texture = rawImageTexture;
    }
}
