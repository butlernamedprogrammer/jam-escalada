using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudMixer : MonoBehaviour
{
    public AudioMixer mix;
    // Start is called before the first frame update
    public void SetVolume(float sliderValue)
    {
        mix.SetFloat("MasterVolume", Mathf.Log10(sliderValue * 20));
    }
}
