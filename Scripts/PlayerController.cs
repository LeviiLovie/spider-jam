using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private string webTag;
    [SerializeField] private string foodTag;

    private WebManager _webManager;
    private Rigidbody2D _rigidbody2D;
    public bool _isGrounded { private set; get; }
    private int _gravityDirection;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _webManager = GetComponent<WebManager>();
    }
    
    void Update()
    {
        Vector2 playerInput = Vector2.zero;
        if (Input.GetKey(KeyCode.A)) //Left
        {
            _rigidbody2D.AddForce(-transform.right * (speed * Time.deltaTime), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.D))// Right
        {
            _rigidbody2D.AddForce(transform.right * (speed * Time.deltaTime), ForceMode2D.Force);
        }
        if (Input.GetKeyDown(KeyCode.Space)) //Jump
        {
            if (_isGrounded)
                _rigidbody2D.AddForce(transform.up*jumpForce);
        }
        if (Input.GetKeyDown(KeyCode.E))// Switch side
        {
            transform.position -= transform.up;
            print("before" +transform.localEulerAngles);
            if (transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < -90)
            {
               
                transform.localEulerAngles += new Vector3(0,0,180);
            }
            else
            {
                transform.localEulerAngles -= new Vector3(0,0,180);
            }
            print("after" +transform.localEulerAngles);
            GravityFlip();
        }
    }

    private void GravityFlip()
    {
        Physics2D.gravity *= -1;
    }
    
    private void GravityChange(Vector2 direction)
    {
        Physics2D.gravity = direction*9.81f;
        print(Physics2D.gravity);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(webTag))
            {
                _isGrounded = true;
                transform.eulerAngles = new Vector3(0, 0,
                    -Mathf.Atan2((other.contacts[0].point.x - transform.position.x),
                        other.contacts[0].point.y - transform.position.y) * 180 / Mathf.PI+180);
                
                if(transform.eulerAngles.z-other.transform.eulerAngles.z<10 && transform.eulerAngles.z-other.transform.eulerAngles.z>-10)
                    transform.eulerAngles = other.transform.eulerAngles;
                else if (transform.eulerAngles.z-180-other.transform.eulerAngles.z<10 && transform.eulerAngles.z-180-other.transform.eulerAngles.z>-10)
                {
                    
                }
                    
                print(transform.eulerAngles.z);
                 
                GravityChange(new Vector2(other.contacts[0].point.x - transform.position.x,other.contacts[0].point.y - transform.position.y));
                
                _webManager.EnableWeb(other.gameObject.GetComponent<BoxCollider2D>());
            }
            if(other.gameObject.CompareTag(foodTag))
            {
               _webManager.AddWeb(3); 
               Destroy(other.gameObject);
            }
        }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag(foodTag))
        {
            Destroy(col.gameObject);
            _webManager.AddWeb(3);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(webTag))
        {
            _isGrounded = false;
            transform.rotation = quaternion.Euler(0, 0, 0, 0);
            Physics2D.gravity = new Vector2(0, -9.81f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Vector3.zero, Physics2D.gravity);
    }
}
