using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float bgSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform rt = this.GetComponent<RectTransform>();
        rt.localPosition += Vector3.right * bgSpeed * Time.deltaTime;
        rt.localPosition -= Vector3.up * bgSpeed * Time.deltaTime;

        if(rt.localPosition.x >= 0)
        {
            rt.localPosition = new Vector3(-400, 225, 0);
        }

    }
}
