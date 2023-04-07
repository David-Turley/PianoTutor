using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseHover : MonoBehaviour
{
    public bool isStart;
    public bool isOptions;
    public bool isMenu;
    public bool isExit;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseDown()
    {
        if (isStart)
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (isMenu)
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (isOptions)
        {
            SceneManager.LoadScene("OptionsMenu");
        }
        if (isExit)
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
