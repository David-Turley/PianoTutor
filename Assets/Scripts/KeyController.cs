using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class KeyController : MonoBehaviour
{
    private SpriteRenderer sr;
    public bool isWhiteKey;
    public InputAction pianoControls;

    // private start and end times. what variable type for real time?

    // set behavior for each game type/mode? start with start time metrenome

    // fixed update check for keypresses, if present check for current note times. use coroutine for duration and end time if needed.

    public KeyCode requiredKey;
    //public bool noteIsPlaying = false;
    private DateTime lastPressed;
    private DateTime lastReleased;

    private TextMeshPro myText;

    private void OnEnable()
    {
        pianoControls.performed += OnPerformed;
        pianoControls.canceled += OnCancelled;
        pianoControls.Enable();
    }

    private void OnDisable()
    {
        pianoControls.performed -= OnPerformed;
        pianoControls.canceled -= OnCancelled;
        pianoControls.Disable();
    }

    void OnPerformed(InputAction.CallbackContext ctx) 
    {
        sr.color = Color.yellow;
        lastPressed = DateTime.Now;
    }

    void OnCancelled(InputAction.CallbackContext ctx) 
    {
        lastReleased = DateTime.Now;
        SetDefaultColor();
    }
    //private Text keyLabel;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        //assign labels values for each key
        //Debug.Log(gameObject.name);
        myText = GetComponentInChildren<TextMeshPro>();

        myText.text = requiredKey.ToString();
        
        //keyLabel.text = requiredKey.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        

        //if (Input.GetKeyDown(requiredKey))
        //{
        //    sr.color = Color.yellow;

        //    lastPressed = DateTime.Now;
        //}

        //if (Input.GetKeyUp(requiredKey))
        //{
        //    SetDefaultColor();

        //    lastReleased = DateTime.Now;
        //}
    }

    public void SetColor(Color c)
    {
        sr.color = c;
    }

    public void SetDefaultColor() 
    {
        if (isWhiteKey == true)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.black;
        }
    }

    public DateTime GetLastPressed()
    {
        return lastPressed;
    }

    public DateTime GetLastReleased()
    {
        return lastReleased;
    }
}
