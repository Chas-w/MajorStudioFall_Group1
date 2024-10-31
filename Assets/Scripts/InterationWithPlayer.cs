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
        interactable = true;
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
            Debug.Log("111");
            GameManager.instance.finalPuzzle.SetActive(true);
            GameManager.instance.FreezeOrUnfreeze();
            interactable = false;
        }

        if (!GameManager.instance.freeze && !GameManager.instance.puzzleSolved)
        {
            interactable = true;
        } 

        if (GameManager.instance.puzzleSolved)
        {
            Destroy(gameObject);
        }
    }
}
