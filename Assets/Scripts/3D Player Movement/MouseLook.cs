using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Data")]
    float sensX; 
    float sensY;

    [Header("External Data")]
    public PlayerMovement pMovement;
    [SerializeField] Transform orientation;

    float xRotation; 
    float yRotation;



    //float startBuffer = 200; 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        sensX = GameManager.instance.horizontalSensitivity;
        sensY = GameManager.instance.verticalSensitivity;
    }

    // Update is called once per frame
    void Update()
    {

        if (!pMovement.haltMovement && !GameManager.instance.freeze)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
