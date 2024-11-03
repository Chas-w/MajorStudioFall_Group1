using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterationWithToy : MonoBehaviour
{
    public float distance;
    public bool interactable;
    public bool pickUp;
    public Button interactionBt;
    RectTransform btRectTrans;
    // Start is called before the first frame update
    void Start()
    {
        interactable = true;
        btRectTrans = interactionBt.GetComponent<RectTransform>();
        pickUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCloseToPlayer();
        Interaction();
        interactionBt.transform.rotation = Quaternion.identity;
    }

    void CheckCloseToPlayer()
    {
        distance = (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(GameManager.instance.player.transform.position.x, 0, GameManager.instance.player.transform.position.z)) * 100f) / 100f;
        InterationButtonVisibility();

    }

    void InterationButtonVisibility()
    {
        Vector3 direction = (transform.position - GameManager.instance.player.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        btRectTrans.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);

        if (distance <= 3f)
        {
            interactionBt.gameObject.SetActive(true);
        }
        else
        {
            interactionBt.gameObject.SetActive(false);
        }
    }

    void Interaction()
    {
        if (interactionBt.gameObject.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            if (pickUp)
            {
                transform.SetParent(null);
                pickUp = false;
            } else
            {
                transform.parent = GameManager.instance.player.transform.GetChild(0);
                pickUp = true;
            }
        }
    }
}
