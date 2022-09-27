using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingCode : MonoBehaviour
{
    [SerializeField,Min(30)]
    int timeBeforeSink;
    BuoyancyEffector2D buoy;
    float decreases;
    // Start is called before the first frame update
    void Start()
    {
        buoy = GetComponent<BuoyancyEffector2D>();
        decreases = buoy.density / (timeBeforeSink/Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        buoy.density -= decreases;

    }
}
