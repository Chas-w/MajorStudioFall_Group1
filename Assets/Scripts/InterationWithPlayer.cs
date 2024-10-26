using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterationWithPlayer : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public bool interactable;
    public Button interactionBt;
    RectTransform btRectTrans;
    // Start is called before the first frame update
    void Start()
    {
        btRectTrans = interactionBt.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCloseToPlayer();
        Interaction();
    }

    void CheckCloseToPlayer()
    {
        distance = (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z)) * 100f) / 100f;
        InterationButtonVisibility();
        
    }

    void InterationButtonVisibility()
    {
        Vector3 direction = (transform.position - player.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        btRectTrans.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);

        if (distance <= 3f)
        {
            interactionBt.gameObject.SetActive(true);
            interactable = true;
        }
        else
        {
            interactionBt.gameObject.SetActive(false);
            interactable = false;
        }
    }

    void Interaction()
    {
        if (interactable && Input.GetKey(KeyCode.F))
        {

        }
    }
}
