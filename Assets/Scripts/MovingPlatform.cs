using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody2D body;
    GameObject blocker;
    // Start is called before the first frame update
    void Start()
    {
        body=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        blocker.gameObject.SetActive(true);
        body.bodyType = RigidbodyType2D.Dynamic;
    }
}
