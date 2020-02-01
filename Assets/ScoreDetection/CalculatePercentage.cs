using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CalculatePercentage : MonoBehaviour
{
    public Texture2D MaskTexture;
    public RenderTexture DrawnTexture;

    public Text Output;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Output.text = CalculateValue().ToString();
        }
    }

    private int CalculateValue()
    {
        var mask = MaskTexture.GetPixels(0, 0, 256, 256);
        var drawn = ToTexture2D(DrawnTexture).GetPixels(0, 0, 256, 256);

        var value = 0f;
        var total = 0f;

        for (var x = 0; x < 256; x++)
        for (var y = 0; y < 256; y++)
        {
            var index = x + y * 256;
            if (mask[index].r < 0.5f)
            {
                total++;
                if (drawn[index].r < 0.5f)
                {
                    value++;
                }
            }
        }

        var result = Mathf.RoundToInt(100f * value / total);

        return result;
    }

    Texture2D ToTexture2D(RenderTexture rTex)
    {
        var tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}