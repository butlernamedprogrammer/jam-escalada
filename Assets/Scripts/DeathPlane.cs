using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public Animator deathPanelAnimator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.otherCollider.tag.Contains("Player"))
        {
            deathPanelAnimator.Play("GameOver");
        }
    }
}
