using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebShoot : MonoBehaviour
{
    [SerializeField] private GameObject _enchorPrefab;
    [SerializeField] private GameObject _webPrefab;

    void Update()
    {
        SpewWebOnMousePosition();
    }
    private void SpewWebOnMousePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mp = GetMousePosition();
            UnableSpringJoin(true);
            SetEnchorPoint(mp);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            _enchorPrefab.SetActive(false);
            UnableSpringJoin(false);
            //have to instantiate web here
            //GameObject web = Instantiate(_webPrefab, this.gameObject);
        }
    }
    private void UnableSpringJoin(bool isActiv)
    {
        this.GetComponent<SpringJoint2D>().enabled = isActiv;
        if (isActiv)
            this.GetComponent<SpringJoint2D>().connectedBody = _enchorPrefab.GetComponent<Rigidbody2D>();
    }
    private void SetEnchorPoint(Vector3 position)
    {
        _enchorPrefab.transform.position = new Vector3(position.x, position.y, 0);
        _enchorPrefab.SetActive(true);
    }
    private Vector2 GetMousePosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);

}
