using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam;
    public bool shake;
    public CinemachineBasicMultiChannelPerlin camShake;
    public Transform camTransform;
    public float frecuencia;
    public float amplitud;
    private Vector3 _originalCamPos;
    public float shakeFreq;

    // Start is called before the first frame update
    void Start()
    {

        shake = false;
        camShake = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (shake == false)
        {
            _originalCamPos = camTransform.position;
            camShake.m_FrequencyGain = 0f;
            camShake.m_AmplitudeGain = 0f;
        }
        else
        {
            camShake.m_FrequencyGain = frecuencia;
            camShake.m_AmplitudeGain = amplitud;
        }
    }

    public void StartShaking()
    {
        shake = true;
    }
    public void StopShaking()
    {
        shake = false;
    }

}
