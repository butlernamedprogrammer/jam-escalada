using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

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
    [SerializeField, Min(1f)]
    float pickupDistance;
    [SerializeField]
    Vector3 pickupOffset;
    [SerializeField]
    float throwPower;
    Rigidbody2D pickedObject;
    bool canJump;
    Vector2 pickingDirection;
    Vector3 previousPosition;
    bool canPickUp;
    Animator anim;
    SpriteRenderer spr;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        canPickUp = true;
        pickingDirection = Vector3.left;
        previousPosition = transform.position;
        pickedObject = null;
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
        if (!canPickUp)
        {
            pickedObject.transform.position = transform.position + pickupOffset;
        }
        CharFlip();
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            Move();
            CheckPickable();
        }

        if (movementInput.x < 0)
        {
            pickingDirection = Vector2.left;
        }
        else if (movementInput.x > 0)
        {
            pickingDirection = Vector2.right;
        }

        ToAnimator();
        previousPosition = transform.position;
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
        if (collision.gameObject.tag.Contains("Level") && collision.transform.position.y < transform.position.y)
        {
            canJump = true;
        }
    }

    private void CheckPickable()
    {
        LayerMask pickMask = LayerMask.GetMask("Pickable");
        Vector2 rayIni = new Vector2(transform.position.x, transform.position.y);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!canPickUp)
            {

                canPickUp = true;
                pickedObject.transform.position = transform.position + new Vector3((pickingDirection.x * pickedObject.transform.localScale.x), 0, 0);
                Rigidbody2D auxBody = pickedObject.GetComponent<Rigidbody2D>();
                auxBody.constraints = RigidbodyConstraints2D.None;
                pickedObject.transform.SetParent(null);
                pickedObject = null;

            }
            else
            {
                RaycastHit2D tryPick;
                if (canPickUp && (tryPick = Physics2D.Raycast(rayIni, pickingDirection, pickupDistance, pickMask)))
                {
                    Debug.Log(transform.localScale.x);
                    canPickUp = false;
                    pickedObject = tryPick.transform.GetComponent<Rigidbody2D>();
                    tryPick.transform.parent = transform;
                    tryPick.transform.position = transform.position + pickupOffset;
                    Rigidbody2D auxBody = tryPick.transform.gameObject.GetComponent<Rigidbody2D>();
                    if (pickedObject.transform.rotation.eulerAngles.z % 90 <= 45)
                    {
                        Vector3 auxRot = pickedObject.transform.rotation.eulerAngles;
                        auxRot.z = 0;
                        pickedObject.transform.rotation = Quaternion.Euler(auxRot);
                    }
                    else if (pickedObject.transform.rotation.eulerAngles.z % 90 > 45)
                    {
                        float extra = 90 - (pickedObject.transform.rotation.eulerAngles.z % 90);
                        Vector3 auxRot = pickedObject.transform.rotation.eulerAngles;
                        auxRot.z += extra;
                        pickedObject.transform.rotation = Quaternion.Euler(auxRot);
                    }
                    auxBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    auxBody.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && !canPickUp)
        {
            canPickUp = true;
            pickedObject.transform.SetParent(null);
            pickedObject.AddForce(pickingDirection * throwPower,ForceMode2D.Impulse);
            pickedObject = null;
        }


    }

    public void ToAnimator()
    {
        anim.SetFloat("MovX", Mathf.Abs((transform.position.x - previousPosition.x) / Time.fixedDeltaTime));
        anim.SetFloat("AnimSpeed Mod", 1 / (Mathf.Abs((transform.position.x - previousPosition.x) / Time.fixedDeltaTime)));
        anim.SetFloat("MovY", (transform.position.y - previousPosition.y) / Time.fixedDeltaTime);
        anim.SetBool("Carrying", !canPickUp);
    }

    public void CharFlip()
    {
        if (pickingDirection.x == 1)
        {
            spr.flipX = true;
        }
        else if (pickingDirection.x == -1)
        {
            spr.flipX = false;
        }
    }

    public void Die()
    {
        dead = true;
    }
}