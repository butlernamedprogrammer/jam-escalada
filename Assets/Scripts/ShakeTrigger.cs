using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTrigger : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam;
    private Animator camAnimator;
    private Rigidbody2D level;
    // Start is called before the first frame update
    void Start()
    {
        camAnimator = cam.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.tag.Contains("Player"))
        {
            collision.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
            collision.gameObject.GetComponent<SinkingCode>().enabled=true;
            if (collision.attachedRigidbody.tag.Contains("Player"))
            {
                camAnimator.Play("Shake");
            }
        }
    }
}
