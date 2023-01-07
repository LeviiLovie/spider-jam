using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int _babycountTotal;
    private int _babycount;

    public int babycountTotal { get { return _babycountTotal; } set { _babycountTotal = value; } }
    public int babycount { get { return _babycount; } set { _babycount = value; } }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScenne()
    {
        SceneManager.LoadScene("Lvl1", LoadSceneMode.Single);
    }
}
