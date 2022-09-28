using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTrigger : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam;
    public SinkingCode sinkBuoyancy;
    private Animator camAnimator;
    public Rigidbody2D level;
    bool firstTime;
    // Start is called before the first frame update
    void Start()
    {
        camAnimator = cam.gameObject.GetComponent<Animator>();
        firstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.tag.Contains("Player")&&firstTime)
        {
            level.bodyType = RigidbodyType2D.Dynamic;
            sinkBuoyancy.enabled=true;
            if (collision.attachedRigidbody.tag.Contains("Player")&&firstTime)
            {
                camAnimator.Play("Shake");
                firstTime = false;
            }
        }
    }
}
