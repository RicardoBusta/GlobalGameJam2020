using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class GameplayUI : MonoBehaviour 
{
    public Text stretching;
    public Text score;

    public string Stretching
    {
        get { return stretching.text; }
        set
        {
            if (!stretching.text.Equals(value)) stretching.text = value;
        }
    }

    public string Score
    {
        get { return score.text; }
        set
        {
            if (!score.text.Equals(value)) stretching.text = value;
        }
    }

}