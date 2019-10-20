using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class RenderWindow : SerializedMonoBehaviour {
    private static RenderWindow Instance;
    
    [SerializeField] private RawImage rawImage;
    [SerializeField] private int resolutionWidth;
    [SerializeField] private int resolutionHeight;
    
    private Texture2D rawImageTexture;

    public int ResolutionWidth => resolutionWidth;
    public int ResolutionHeight => resolutionHeight;

    private void Awake() {
        if (Instance != null) Application.Quit();

        Instance = this;
        rawImageTexture = rawImageTexture != null ? rawImageTexture : new Texture2D(resolutionWidth, resolutionHeight);
    }

    public void SetPixels(Color[] colors) {
        rawImageTexture = rawImageTexture != null ? rawImageTexture : new Texture2D(resolutionWidth, resolutionHeight);
        rawImageTexture.SetPixels(colors);
        rawImageTexture.Apply();
        rawImage.texture = rawImageTexture;
    }
}
