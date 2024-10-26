using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleContent : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public RectTransform windowsRect;
    public Vector2 gridSizePerPage;
    public float nodeCellSize;
    public float musicSpeed;
    public List<Node> nodes = new List<Node>();

    RectTransform rectTransform;
    bool puzzleStart;
    float startY, endY;
    //public float startPosY;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        //reset content pos
        startY = -windowsRect.sizeDelta.y / 2;
        rectTransform.sizeDelta = new Vector2(0, nodes.Count * (windowsRect.sizeDelta.y / gridSizePerPage.y));
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY);
        endY = startY + rectTransform.sizeDelta.y;

        //generate node based on input
        GenerateNode();

        puzzleStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        //hit space to start
        if (Input.GetKeyDown(KeyCode.Space))
        {
            puzzleStart = true;
        }
        StartPuzzle();

        StopPuzzle();
    }

    void StopPuzzle()
    {
        if (rectTransform.anchoredPosition.y >= endY)
        {
            musicSpeed = 0f;
        }
    }

    void StartPuzzle()
    {
        
        if (puzzleStart)
        {
            rectTransform.anchoredPosition += new Vector2(0, musicSpeed * Time.fixedDeltaTime);
            //play audio if it is filled
            for (int i = 0; i < transform.childCount; i++)
            {
                if (Mathf.Abs(rectTransform.anchoredPosition.y - transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition.y) < 1f)
                {
                    Debug.Log("1111");
                    if (nodes[i].isFilled)
                    {
                        audioSource.clip = nodes[i].audioClip;
                        audioSource.Play();
                    } else
                    {
                        musicSpeed = 0f;
                        /*
                        if (Input.GetKeyDown(KeyCode.a)) 
                        {
                            //if correct(the number is equal to node.pitch)
                            ***musicSpeed = 1f;
                            //if not correct
                            ***show bad effect and exit?
      
                        }
                        */
                    }
                }
            }
        }
    }

    void GenerateNode()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            GameObject node = new GameObject("Node");
            node.transform.parent = transform;
            Image image = node.AddComponent<Image>();
            image.sprite = nodes[i].image;
            RectTransform nodeRect = node.GetComponent<RectTransform>();
            nodeRect.anchorMin = new Vector2(0, 1);
            nodeRect.anchorMax = new Vector2(0, 1);
            nodeRect.sizeDelta = new Vector2(nodeCellSize, nodeCellSize);
            nodeRect.anchoredPosition = new Vector2(((nodes[i].pitch - 1) * 2 + 1) * windowsRect.sizeDelta.x / gridSizePerPage.x / 2, -(i * 2 + 1) * windowsRect.sizeDelta.y / gridSizePerPage.y / 2);
        }

    }
}


[System.Serializable]
public class Node
{
    public bool isFilled; //whether this node is going to be filled by player
    public int pitch;
    public Sprite image;
    public AudioClip audioClip;
}