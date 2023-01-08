using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WebManager : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private GameObject web;
    private PlayerController _playerController;
    private List<BoxCollider2D> _webs;
    private Vector3 _playerPointOffset;
    [SerializeField] private TextMeshProUGUI webText;
    
    
    [SerializeField] public int webAmount;
    private void Start()
    {

        webText.text = "Webs: " + webAmount;
        _webs = new List<BoxCollider2D>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) & _playerController._isGrounded)
        {
            
            var mouse = Input.mousePosition;
            _playerPointOffset =(transform.up * offset) + transform.position;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null & webAmount>0)
            {
                if(hit.collider.gameObject.CompareTag("Web") || hit.collider.gameObject.CompareTag("Obstacle"))
                {   
                    webAmount--;
                    webText.text = "Webs: " + webAmount;
                    Vector2 newPos = Vector2.Lerp(castPoint.origin, _playerPointOffset,0.5f);
                    GameObject newWeb = Instantiate(web,newPos,Quaternion.identity);
                            
                    newWeb.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 
                        Mathf.Atan2(castPoint.origin.y - _playerPointOffset.y, 
                            castPoint.origin.x - _playerPointOffset.x) * Mathf.Rad2Deg);
                            
                    newWeb.transform.localScale = new Vector3(Vector2.Distance(castPoint.origin,_playerPointOffset),0.1f,1);
                    _webs.Add(newWeb.GetComponent<BoxCollider2D>());
                }
            }
            
            
        }
        if (_playerController._isGrounded)
            return;
        
        foreach (var web in _webs)
        {
            web.enabled = true;
        }
        
    }

    public void EnableWeb(BoxCollider2D box)
    {
        foreach (var web in _webs)
        {
            if (box != web)
            {
                web.enabled = false;
                
            }
        }
    }

    public void AddWeb(int amount)
    {
        webAmount += amount;
        webText.text = "Webs: " + webAmount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_playerPointOffset,0.1f);
    }
}
