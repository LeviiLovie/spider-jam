using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float MovementSpeed;
    public float JumpSpeed;
    public GameObject SpawnPoint;
    public KeyCode RespawnKeyCode;

    [Header("Web Settings")]
    public float WebMovingSpeed;
    public float WebLenght;
    public GameObject MousePositionObject;
    public KeyCode WebUsingKeyCode;
    public LayerMask WebLayers;

    // private bool isWeb;
    // private Vector2 webVector2;
    private Vector2 movingVector2;
    private Vector2 mousePositionInUnits;

    // void Start()
    // {
    //     if (this.GetComponent<Rigidbody2D>())
    //     {
    //         GetComponent<Rigidbody2D>().freezeRotation = true;
    //     }
    // }

    void FixedUpdate()
    {
        // if (!isWeb) {
            movingVector2 = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") * JumpSpeed);
            transform.Translate(movingVector2 * Time.deltaTime * MovementSpeed, Space.World);
           
            mousePositionInUnits = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MousePositionObject.transform.position = mousePositionInUnits;
        // }
        // else {
        //     Vector2 position = transform.position;
        //     Debug.Log((webVector2 - position).magnitude + " - " + WebMovingSpeed);
        //     if ((webVector2 - position).magnitude < WebMovingSpeed) {
        //         transform.position = webVector2;
        //         isWeb = false;
        //     } else {
        //         Vector2 webVector2Normalized = webVector2;
        //         webVector2Normalized.Normalize();
        //         Debug.Log(webVector2Normalized + " - " + webVector2);
        //         transform.Translate(webVector2Normalized * WebMovingSpeed);
        //     }
        // }
    }

    void Update()
    {
        if (Input.GetKeyDown(WebUsingKeyCode))
        {
            // Debug.Log("F");
            Vector2 startPosition = transform.position;
            // RaycastHit2D hit = Physics2D.Raycast(startPosition + new Vector2(0, -1), mousePositionInUnits, 1000.0f);
            RaycastHit2D hit = Physics2D.Raycast(startPosition, mousePositionInUnits - startPosition, 1000.0f, WebLayers);
            if (hit.collider != null)
            {
                Debug.Log(hit.point);
                // Debug.DrawRay(startPosition, hit.point, Color.yellow);
                // transform.position = hit.point - new Vector2(0, -1);
                // isWeb = true;
                Vector2 webVector2Normalized = hit.point - startPosition;
                webVector2Normalized.Normalize();
                this.GetComponent<Rigidbody2D>().AddForce(webVector2Normalized * WebMovingSpeed);
                // transform.position = hit.point;
            }
        }
        else if (Input.GetKeyDown(RespawnKeyCode))
        {
            transform.position = SpawnPoint.transform.position;
            // isWeb = false;
        }
    }
}
