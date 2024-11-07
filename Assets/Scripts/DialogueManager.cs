using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI contentText;
    [SerializeField] Conversation conversation;
    CanvasGroup DialogueGroup;

    public int contentIndex;
    private void Start()
    {
        DialogueGroup = GetComponent<CanvasGroup>();
        StartDialogue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (contentIndex == conversation.content.Count - 1)
            {
                EndDialogue();
            } else
            {
                UpdateDialogue();
            }
        }
    }

    public void UpdateDialogue()
    {
        contentIndex++;
        contentText.text = conversation.content[contentIndex];
    }

    void EndDialogue()
    {
        //animation
        StartCoroutine(Twenning.StartTweening(
            TweeningCurve.Linear, 
            1f, 
            (t) =>
                {
                    DialogueGroup.alpha = 1 - t;
                }, 
            () => {
                gameObject.SetActive(false);
                }));
    }
    

    public void StartDialogue()
    {
        contentIndex = 0;
        //animation
        StartCoroutine(Twenning.StartTweening(TweeningCurve.Linear, 1f, (t) =>
        {
            DialogueGroup.alpha = t;
        }));

        contentText.text = conversation.content[contentIndex];
    }
}

[Serializable]
public class Conversation
{
    public List<string> content = new List<string>();
}
