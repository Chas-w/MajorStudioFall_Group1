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

    [Header("Lighting")]
    public Light point;
    [SerializeField] float intensityMax;
    [SerializeField] float lightIntensity; 

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
        point.intensity = lightIntensity; 
    }


    private void SonarDetection()
    {
        Vector3 detectorScale = detector.localScale;



        if (gameManager.instrumentIsPlaying)
        {
            if (lightIntensity < intensityMax)
            {
                lightIntensity += Time.deltaTime * 2f; 
            } if (lightIntensity >= intensityMax)
            {
                lightIntensity = intensityMax;
            }
            detectorScale.x += Time.deltaTime * detectorSpeed;
            detector.localScale = detectorScale;
        } 
        
        if (!gameManager.instrumentIsPlaying)
        {
            if (lightIntensity > 0f)
            {
                lightIntensity -= Time.deltaTime * 4f;
            }
            if (lightIntensity <= 0f)
            {
                lightIntensity = 0f;
            }
            detectorScale.x -= Time.deltaTime * (detectorSpeed * 2);
            if (detectorScale.x <= 1)
            {
                detectorScale = Vector3.zero;
            }
            detector.localScale = detectorScale;
    
        }
        

    }
}
