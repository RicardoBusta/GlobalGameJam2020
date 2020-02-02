using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour
{
    public Sprite MaskSprite;
    public RenderTexture DrawnTexture;

    private const int ImageSide = 256;

    public int FinalScore;
    
    public void UpdateValue()
    {
        FinalScore = CalculateValue();
    }

    private int CalculateValue()
    {
        var mask = MaskSprite.texture.GetPixels(0, 0, ImageSide, ImageSide);
        var drawn = ToTexture2D(DrawnTexture).GetPixels(0, 0, ImageSide, ImageSide);

        var value = 0f;
        var total = 0f;

        for (var x = 0; x < ImageSide; x++)
        for (var y = 0; y < ImageSide; y++)
        {
            var index = x + y * ImageSide;
            if (mask[index].a > 0.5f)
            {
                total++;
                if (drawn[index].a > 0.5f)
                {
                    value++;
                }
            }
        }

        var result = Mathf.RoundToInt(100f * value / total);

        return result;
    }

    private Texture2D ToTexture2D(RenderTexture rTex)
    {
        var tex = new Texture2D(ImageSide, ImageSide, TextureFormat.RGBA32, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}