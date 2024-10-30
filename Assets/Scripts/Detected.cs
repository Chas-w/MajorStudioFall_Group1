using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detected : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    bool played; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Detector") && !played)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Detector") && !played)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            played = false;
        }
    }
}
