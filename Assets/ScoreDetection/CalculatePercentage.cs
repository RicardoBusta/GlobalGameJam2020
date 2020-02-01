﻿using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CalculatePercentage : MonoBehaviour
{
    public Texture2D MaskTexture;
    public RenderTexture DrawnTexture;

    public Text Output;

    private const int ImageSide = 256;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Output.text = CalculateValue().ToString();
        }
    }

    private int CalculateValue()
    {
        var mask = MaskTexture.GetPixels(0, 0, ImageSide, ImageSide);
        var drawn = ToTexture2D(DrawnTexture).GetPixels(0, 0, ImageSide, ImageSide);

        var value = 0f;
        var total = 0f;

        for (var x = 0; x < ImageSide; x++)
        for (var y = 0; y < ImageSide; y++)
        {
            var index = x + y * ImageSide;
            if (mask[index].r > 0.5f)
            {
                total++;
                if (drawn[index].r > 0.5f)
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
        var tex = new Texture2D(ImageSide, ImageSide, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}