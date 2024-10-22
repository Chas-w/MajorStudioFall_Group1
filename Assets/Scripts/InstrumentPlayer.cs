using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstrumentPlayer: MonoBehaviour
{
    [Header("Instrument Print Data")]
    public string notesPlayed;

    [Header("External Data")]
    public int noteCounterMax;
    public int noteCount;
    public bool sameNotes; 
    public bool pluck;

    [Header("Instrument Note Data")]
    [SerializeField] string[] notesCollection;
    [SerializeField] float tempoMax;
    public float tempo;
    public bool asending;
    public bool descending; 

    [Header("Instrument Audio Data")]
    [SerializeField] AudioSource instrument;
    [SerializeField] AudioClip[] instrumentNotes;
    [SerializeField] KeyCode[] instrumentInput;
    [SerializeField] KeyCode[] instrumentInputAlt; 

  
    float pluckTimerMax = .05f; 
    float pluckTimer;

    float liftTimerMax = 5f; 
    float liftTimer; 

    int noteCounter;

    int index;
    int lastIndex;
    int firstIndex; 
    int upDownCheck; 

    // Start is called before the first frame update
    void Start()
    {
        pluckTimer = pluckTimerMax; 
    }

    // Update is called once per frame
    void Update()
    {
        PlayNotes();

    }
    private void FixedUpdate()
    {
        TempoCheck();
        if (asending)
        {
            liftTimer-= Time.deltaTime;
        }
        if (liftTimer <= 0 || descending)
        {
            asending = false;
            descending = true; 
            liftTimer = liftTimerMax;
        }
    }

    void PlayNotes()
    {
        tempo -= MathF.Abs(Time.deltaTime);
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                if (instrumentInput.Contains(vKey) || instrumentInputAlt.Contains(vKey))
                {
                    for (int i = 0; i < instrumentInput.Length; i++)
                    {
                        if (instrumentInput[i] == vKey || instrumentInputAlt[i] == vKey) { index = i; break; }
                    }

                    pluck = true;
                    tempo = tempoMax;

                    noteCounter++;
                    noteCount++;
                    upDownCheck++;
                    CheckUpDown();


                    instrument.PlayOneShot(instrumentNotes[index]);
                    SaveNotes();

                    
                }
               
            }
           
        }
        if (pluck)
        {
            //*****************New Code*****************
            GameManager.instance.instrumentIsPlaying = true;


            pluckTimer -= Time.deltaTime; 
            if (pluckTimer <= 0)
            {
                pluck = false;
                pluckTimer = pluckTimerMax; 
            }
        } 
    }
    void SaveNotes()
    {
        
        if (noteCounter <= noteCounterMax)
        {
            notesPlayed += notesCollection[index];
        }

        if (noteCounter > noteCounterMax )
        {
            notesPlayed = "";
            noteCounter = 0;
        }
    }
    void TempoCheck()
    {
        if (tempo <= 0)
        {
            notesPlayed = "";
            noteCounter = 0;
            noteCount = 0; 
            upDownCheck = 0;
        }
        
    }
    void CheckUpDown()
    {
        if (upDownCheck == 1)
        {
            firstIndex = index;
        }
        if (upDownCheck == 2)
        {
            lastIndex = index;
        }
        if (upDownCheck == 3 || upDownCheck == 0)
        {
            if ((index - lastIndex > 0) && (lastIndex - firstIndex > 0))
            {
                liftTimer = liftTimerMax;
                asending = true;
                descending = false; 
            }
            if ((index - firstIndex < 0) && (lastIndex - firstIndex < 0))
            {
                asending = false;
                descending = true;
            }
            if (((index - firstIndex <= 0) && (lastIndex - firstIndex >= 0)) || ((index - firstIndex >= 0) && (lastIndex - firstIndex <= 0)))
            {
                asending = false;
                descending = false;
            }
            if (index == lastIndex &&  index == firstIndex) {sameNotes = true; } else { sameNotes = false; }
            upDownCheck = 0;
        }


    }

}
