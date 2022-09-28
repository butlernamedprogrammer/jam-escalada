using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam;
    [SerializeField, Min(1)]
    float shakeTime;
    public bool shake;
    public CinemachineBasicMultiChannelPerlin camShake;
    public Transform camTransform;
    public float frecuencia;
    public float amplitud;
    private Vector3 _originalCamPos;
    public float shakeFreq;
    private float shakeEndTime;
    bool afterShake;
    // Start is called before the first frame update
    void Start()
    {
        afterShake = false;
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
        if (Time.unscaledTime > shakeEndTime && !afterShake)
        {
            _originalCamPos = camTransform.position;
            camShake.m_FrequencyGain = 0f;
            camShake.m_AmplitudeGain = 0f;
            afterShake = true;
        }
    }

    public void StartShaking()
    {
        shake = true;
        shakeEndTime = Time.unscaledTime + shakeTime;
    }
    public void StopShaking()
    {
        shake = false;
    }

}
