using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManagerScript : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(SetLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat("RubberPitch", sliderValue);
    }
}
