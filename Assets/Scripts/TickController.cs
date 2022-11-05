using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickController : MonoBehaviour
{
    private SpriteRenderer sr;
    private string nextKeyName;
    public Text nextKeyText;
    public Text scoreText;
    private bool scored = false;
    private int scoreRangeMs = 800, tickFrequencyMs = 1000;

    private AudioSource tickSound;

    private GameObject[] pianoKeys;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        pianoKeys = GameObject.FindGameObjectsWithTag("key");

        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayTick());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        scoreText.text = score.ToString();
    }

    //call when player successfully hits note
    private void DidHit() 
    {
        score = score + 1;
        Debug.Log("Score increased: " + score.ToString());

        scored = true;
    }

    //call when player misses note
    private void MissedHit() 
    {
        scored = true;
        Debug.Log("Failed to score.");
    }

    public void SetScoreRange(int ms)
    { 
        scoreRangeMs = ms;
    }

    public void SetTickFrequency(int ms)
    { 
        tickFrequencyMs = ms;
    }

    private int GetNextIndex(int index)
    {
        int nextIndex;

        //ensure next key isn't the same as previous key to prevent highlighting conflicts
        do
        {
            nextIndex = UnityEngine.Random.Range(0, pianoKeys.Length);
        } while (index == nextIndex);
        //Debug.Log("next index in function: " + nextIndex.ToString());

        return nextIndex;
    }

    IEnumerator PlayTick() 
    {
        DateTime lastTick = DateTime.Now;
        int index, nextIndex = -1;
        //bool scored = false;

        tickSound = GetComponent<AudioSource>();

        KeyController currentKey = null, nextKey = null;

        while (true)
        {
            yield return new WaitForSeconds(.01f);

            //not yet x ms since last tick, but more than y ms
            if (lastTick.AddMilliseconds(tickFrequencyMs) > DateTime.Now && lastTick.AddMilliseconds(scoreRangeMs) < DateTime.Now)
            {
                // reset metronome color
                if (sr.color != Color.white)
                {
                    sr.color = Color.white;
                }

                // failed to score
                if (scored == false)
                { 
                    MissedHit();

                    if (currentKey != null)
                    {
                        currentKey.SetDefaultColor();
                    }
                }

            }
            //1 second has passed. reset tick.
            else if (lastTick.AddMilliseconds(tickFrequencyMs) <= DateTime.Now)
            {
                //play sound
                tickSound.Play(0);

                //metronome
                sr.color = Color.yellow;

                //set the current index to the next value. random value if next index not yet set.
                if (nextIndex != -1)
                {
                    index = nextIndex;
                }
                else
                {
                    index = UnityEngine.Random.Range(0, pianoKeys.Length);
                }
                
                //calculate the next index
                nextIndex = GetNextIndex(index); 
                
                Debug.Log("next index out of function: " + nextIndex.ToString());

                //set and highlight the next key
                nextKeyName = pianoKeys[nextIndex].name;
                nextKey = pianoKeys[nextIndex].GetComponent<KeyController>();
                nextKey.SetColor(Color.green);

                //set the current key that must be hit to be scored
                currentKey = pianoKeys[index].GetComponent<KeyController>();
                currentKey.SetColor(Color.red);

                //set the next key text label
                nextKeyText = GameObject.Find("NextKeyText").GetComponent<Text>();
                nextKeyText.text = String.Format("Next key is: " + nextKeyName);

                
                lastTick = DateTime.Now;
                scored = false;

            }

            if (currentKey != null)
            {
                //between x ms of tick going off counts as scored
                if (scored == false 
                    && (lastTick.AddMilliseconds(scoreRangeMs) > currentKey.GetLastPressed()) 
                    && (lastTick < currentKey.GetLastPressed())
                   )
                {
                    DidHit();

                    currentKey.SetDefaultColor();
                }
            }
        }
    }
}
