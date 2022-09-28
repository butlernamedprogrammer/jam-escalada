using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudMixer : MonoBehaviour
{
    public AudioMixer mix;
    Slider slider;
    // Start is called before the first frame update
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void SetVolume(float soundLevel)
    { 
        mix.SetFloat("musicVol", soundLevel);
    }
}
