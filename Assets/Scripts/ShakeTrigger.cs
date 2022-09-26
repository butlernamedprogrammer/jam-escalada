using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTrigger : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam;
    private Animator camAnimator;
    // Start is called before the first frame update
    void Start()
    {
        camAnimator = cam.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.tag.Contains("Player"))
        {   
            camAnimator.Play("Shake");
        }
    }
}
