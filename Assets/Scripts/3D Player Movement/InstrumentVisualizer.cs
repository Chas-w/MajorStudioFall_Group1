using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentVisualizer : MonoBehaviour
{
    // Start is called before the first frame update
    public InstrumentPlayer instrumentPlayer;
    [SerializeField] GameObject handLeft;
    [SerializeField] GameObject handRight;
    [SerializeField] Transform[] positionLeft;
    [SerializeField] Transform[] positionRight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (instrumentPlayer.pluck)
        {
            int rightIndex = Random.Range(0, positionRight.Length); 
            int leftIndex = Random.Range(0, positionLeft.Length);

            handRight.transform.position = positionRight[rightIndex].position;
            handLeft.transform.position = positionLeft[leftIndex].position;
        }
    }
}
