using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTest : MonoBehaviour
{
    [Header("Instrument Data")]
    InstrumentPlayer instrumentPlayer;

    [Header("Song Data")]
    public int songLength;
    public string songNotes;
    [SerializeField] bool listening; 


    // Start is called before the first frame update
    void Start()
    {
        instrumentPlayer = GameManager.instance.instrument;
    }

    // Update is called once per frame
    void Update()
    {
 
        if (listening)     
        {
            instrumentPlayer.noteCounterMax = songLength;
            if (instrumentPlayer.notesPlayed == songNotes)
            {
                Debug.Log("You played the right song Orpheus");
            }
            else
            {
                Debug.Log("Not quite right");
            }
        }
    

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          //  instrumentPlayer = other.GetComponent<InstrumentPlayer>();
            listening = true; 
        } 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          //  instrumentPlayer = null; 
            listening = false;
        }
    }
}
