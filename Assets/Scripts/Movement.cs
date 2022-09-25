using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D body;
    Vector2 movementInput;
    bool dead;
    [SerializeField, Min(4f)]
    float movementSpeed;
    [SerializeField]
    float decelerationSpeed = 0.5f;
    [SerializeField, Min(3)]
    int jumpForce;
    [SerializeField]
    Vector2 pickupOffset;
    [SerializeField]
    Transform pickedObject;
    bool canJump;

    bool canPickUp;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        canPickUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
        if (dead)
        {
            movementInput = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        Move();
        transform.forward = -Camera.main.transform.forward;
        if (Input.GetKeyDown(KeyCode.E) && !dead)
        {
            CheckPickable();
        }
    }

    private void Move()
    {
        if (movementInput.x == 0)
        {
            body.velocity = new Vector2(Mathf.Lerp(body.velocity.x, 0, decelerationSpeed * Time.deltaTime), body.velocity.y);
        }
        body.AddForce(movementInput * movementSpeed, ForceMode2D.Force);
        if (Input.GetKey(KeyCode.Space) && !dead)
        {
            Jump();
        }

    }

    private void Jump()
    {
        if (canJump)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Level"))
        {
            canJump = true;
        }
    }

    private void CheckPickable()
    {
        Vector2 pickingDirection = movementInput;
        pickingDirection.y = 0;
        Vector3 pickOffset = pickupOffset;
        Vector2 rayIni = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D tryPick = Physics2D.Raycast(rayIni + pickingDirection, pickingDirection, 1f);
        if (!canPickUp)
        {

            canPickUp = true;
            pickedObject.transform.position = transform.position + new Vector3(pickingDirection.x + pickedObject.localScale.x, 0, 0);
            pickedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            pickedObject.SetParent(null);
            pickedObject = null;
        }
        if (tryPick.transform.tag.Contains("Pickable") && canPickUp)
        {
            canPickUp = false;
            pickedObject = tryPick.transform;
            tryPick.transform.parent = transform;
            tryPick.transform.position = transform.position + pickOffset;
            Rigidbody2D aux = tryPick.transform.gameObject.GetComponent<Rigidbody2D>();
            aux.bodyType = RigidbodyType2D.Static;
        }
    }

    public void Die()
    {
        dead = true;
    }
}