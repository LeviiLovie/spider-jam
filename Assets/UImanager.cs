using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UImanager : MonoBehaviour
{
    [SerializeField] private TMP_Text babysCountTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        babysCountTxt.text = $"Babys : {GameManager.instance.babycount.ToString()} / {GameManager.instance.babycountTotal.ToString()}";
    }
}
