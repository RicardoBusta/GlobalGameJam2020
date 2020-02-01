using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
public sealed class ScrollMaterialOffset : MonoBehaviour 
{
    public float scrollSpeed = 0.5f;

    private Material material;
    private Vector2 offset = Vector2.zero;
    private readonly int MAIN_TEXTURE_OFFSET = Shader.PropertyToID("_MainTex");

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset.x += Time.deltaTime * scrollSpeed;
        material.SetTextureOffset(MAIN_TEXTURE_OFFSET, offset);
    }
}