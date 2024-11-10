using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_Steps : MonoBehaviour
{
    [SerializeField] float stepTimer = .75f;
    [SerializeField] float stepTimerMax = .75f;
    [SerializeField] AudioSource stepSource;
    [SerializeField] AudioClip[] footsteps; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stepTimer -= Time.deltaTime;
        if (stepTimer <= 0)
        {
            stepSource.PlayOneShot(footsteps[(Random.Range(0, footsteps.Length))], 2);
            stepTimer = stepTimerMax;
        }
    }
}
