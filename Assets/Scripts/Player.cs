using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float MovementSpeed;
    public float JumpSpeed;

    private bool isWeb;
    private bool canWalk;
    private bool isJump;
    private Vector2 webVector2;
    private Vector2 movingVector2;
    private Vector2 mousePositionInUnits;

    private Rigidbody2D _rigitBody2d;

    void Start()
    {
        if (this.GetComponent<Rigidbody2D>())
        {
            _rigitBody2d = GetComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }

    void FixedUpdate()
    {
        //if (isWeb) {
        //    Physics.gravity = new Vector3(0, 0, 0);

        //    movingVector2 = (Point1.transform.position - Point2.transform.position);
        //    movingVector2.Normalize();
        //    Debug.Log(movingVector2);
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        _rigitBody2d.AddForce(movingVector2);
        //    }
        //    else if (Input.GetKey(KeyCode.D))
        //    {
        //        _rigitBody2d.AddForce(-movingVector2);
        //    }
        //}
        //else
        //{
        //    Physics.gravity = new Vector3(0, -1.0F, 0);

        //}

        if (canWalk) {
            if (Input.GetKey(KeyCode.A))
            {
                _rigitBody2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * MovementSpeed * Time.deltaTime, _rigitBody2d.velocity.y);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _rigitBody2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * MovementSpeed * Time.deltaTime, _rigitBody2d.velocity.y);
            }
            else
            {
                _rigitBody2d.velocity = new Vector2(0, _rigitBody2d.velocity.y);
            }

            if (Input.GetKey(KeyCode.Space) && !isJump)
            {
                _rigitBody2d.AddForce(this.transform.up * JumpSpeed, ForceMode2D.Impulse);
                Debug.Log(this.transform.position + " pose");
                Debug.Log(this.transform.up + " : up");
                isJump = true;
            }
        } else {
            // _rigitBody2d.velocity = new Vector2(0, -9.8f);
        }
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(WebUsingKeyCode))
    //     {
    //         Vector2 startPosition = transform.position;
    //         RaycastHit2D hit = Physics2D.Raycast(startPosition, mousePositionInUnits - startPosition, 1000.0f, WebLayers);
    //         if (hit.collider != null)
    //         {
    //             Debug.Log(hit.point);
    //             Vector2 webVector2Normalized = hit.point - startPosition;
    //             webVector2Normalized.Normalize();
    //             this.GetComponent<Rigidbody2D>().AddForce(webVector2Normalized * WebMovingSpeed);
    //         }
    //     }
    // }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("enter");
        if (other.gameObject.tag == "home")
        {
            // isWeb = true;
            canWalk = true;
            Debug.Log("home");
        }
        isJump = false;
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "home")
        {
            // isWeb = false;
            canWalk = false;
        }
        isJump = true;
    }
}
