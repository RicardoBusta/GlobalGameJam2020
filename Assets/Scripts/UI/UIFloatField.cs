using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class UIFloatField : MonoBehaviour 
{
    [SerializeField] private Text valueText;

    private float floatValue;

    public float Value
    {
        get { return floatValue; }
        set
        {
            if (Mathf.Approximately(floatValue, value)) return;
            floatValue = value;
            valueText.text = floatValue.ToString();
        }
    }

    private void Reset () 
	{
        valueText = transform.Find("ValueText")?.GetComponent<Text>();
    }	
}