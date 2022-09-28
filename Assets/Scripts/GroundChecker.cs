using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool onGround;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag.Contains("Level"))
        {
            onGround=true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.transform.tag.Contains("Level"))
        {
            onGround = false;
        }
    }

    public bool IsOnGround()
    {
        return onGround;
    }
}
