using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public sealed class SortingSprite : MonoBehaviour 
{
    [SerializeField] private ScoreCalculator calculator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;


    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake() 
	{
        Sort();
    }

    public void Sort()
    {
        if (sprites.Length == 0) return;

        int index = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[index];
        calculator.MaskSprite = sprites[index];
    }
}