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

    private Transform closestToy;
    // Start is called before the first frame update
    void Start()
    {
        interactable = false;
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

    Transform GetClosestChild()
    {
        Transform closestChild = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (GameObject child in GameManager.instance.toyList)
        {
            float distanceSqr = (child.transform.position - GameManager.instance.player.transform.position).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestChild = child.transform;
            }
        }

        return closestChild;
    }

    void CheckCloseToPlayer()
    {
        closestToy = GetClosestChild();
        InterationButtonVisibility();

    }

    void InterationButtonVisibility()
    {
        Vector3 direction = (transform.position - GameManager.instance.player.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        btRectTrans.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);

        distance = Vector3.Distance(closestToy.transform.position, GameManager.instance.player.transform.position);
        if (distance <= 5f)
        {
            interactable = true;
        }
        else
        {
            interactable = false;
        }

        interactionBt.gameObject.SetActive(interactable);
    }

    void Interaction()
    {
        if (interactionBt.gameObject.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            if (pickUp)
            {
                closestToy.transform.SetParent(null);
                pickUp = false;
            } else
            {
                closestToy.transform.parent = GameManager.instance.player.transform.GetChild(0);
                pickUp = true;
            }
        }
    }
}
