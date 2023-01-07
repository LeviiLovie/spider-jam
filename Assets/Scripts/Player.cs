using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float MovementSpeed;
    public float JumpSpeed;
    public GameObject Point1;
    public GameObject Point2;

    private bool isWeb;
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

    void Update()
    {
        if (isWeb) {
            Physics.gravity = new Vector3(0, 0, 0);

            movingVector2 = (Point1.transform.position - Point2.transform.position);
            movingVector2.Normalize();
            Debug.Log(movingVector2);
            if (Input.GetKey(KeyCode.A))
            {
                _rigitBody2d.AddForce(movingVector2);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _rigitBody2d.AddForce(-movingVector2);
            }
        }
        else
        {
            Physics.gravity = new Vector3(0, -1.0F, 0);
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
        if (other.gameObject.tag == "web")
        {
            isWeb = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "web")
        {
            isWeb = false;
        }
    }
}
