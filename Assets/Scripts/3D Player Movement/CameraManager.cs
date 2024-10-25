using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform normalPos;
    [SerializeField] Transform talkPos;


    [SerializeField] float moveSpeed; 


    public bool moveTalk;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (moveTalk)
        {    
            TalkCamTransform(); 
        }
        else
        {
            NormCamTransform(); 
        }
    }

    private void TalkCamTransform()
    {
        transform.position = Vector3.Lerp(transform.position, talkPos.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, talkPos.rotation, moveSpeed * Time.deltaTime);
    }
    private void NormCamTransform()
    {
        transform.position = Vector3.Lerp(transform.position, normalPos.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, normalPos.rotation, moveSpeed * Time.deltaTime);

    }

}
