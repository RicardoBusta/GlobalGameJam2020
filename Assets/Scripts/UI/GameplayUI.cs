using UnityEngine;

[DisallowMultipleComponent]
public sealed class GameplayUI : MonoBehaviour 
{
    [SerializeField] private UIFloatField stretching;
    [SerializeField] private UIFloatField score;

    public void SetStretching(float value)
    {
        stretching.Value = value;
    }

    public void SetScore(float value)
    {
        stretching.Value = value;
    }
}