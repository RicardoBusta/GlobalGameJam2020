using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CalculatePercentage : MonoBehaviour
{
    public Texture2D TargetTexture;
    public RenderTexture CheckTexture;

    public Text Output;

    public void Update()
    {
        var target = TargetTexture.GetPixels(0, 0, 256, 256);
        var check = ToTexture2D(CheckTexture).GetPixels(0, 0, 256, 256);

        var value = 0;

        for (var x = 0; x < 256; x++)
        for (var y = 0; y < 256; y++)
        {
            var index = x + y * 256;
            value += (int) check[index].r;
        }

        Output.text = value.ToString();
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