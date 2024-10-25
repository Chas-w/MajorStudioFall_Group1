using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcoPolo : MonoBehaviour
{

    public GameManager gameManager;

    [Header("Detecting Data")]
    public Transform detector;
    [SerializeField] float detectTime; 
    [SerializeField] float detectorSpeed;
    [SerializeField] float detectorMax;
    [SerializeField] float detectorMin;

    [Header("Detecting States")]
    [SerializeField] bool gotBig;
    [SerializeField] bool gotSmall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SonarDetection(); 
    }


    private void SonarDetection()
    {
        Vector3 detectorScale = detector.localScale;



        if (gameManager.instrumentIsPlaying)
        {

            detectorScale.x += Time.deltaTime * detectorSpeed;
            detector.localScale = detectorScale;
        } 
        
        if (!gameManager.instrumentIsPlaying)
        {
            detectorScale.x -= Time.deltaTime * detectorSpeed;
            if (detectorScale.x <= 1)
            {
                detectorScale = Vector3.zero;
            }
            detector.localScale = detectorScale;
    
        }
        

    }
}
