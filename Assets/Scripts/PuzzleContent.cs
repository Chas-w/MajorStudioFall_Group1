using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleContent : MonoBehaviour
{
    // Start is called before the first frame update
    public InstrumentPlayer instrument;
    public AudioSource audioSource;
    public RectTransform windowsRect;
    Vector2 gridSizePerPage;
    float nodeCellSize;
    float musicSpeed;
    List<Node> nodes = new List<Node>();

    RectTransform rectTransform;
    bool puzzleStart;
    float startY, endY;
    float distanceMoved;
    int index;
    //public float startPosY;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        //set variables based on GameManager
        gridSizePerPage = GameManager.instance.gridSizePerPage;
        nodeCellSize = GameManager.instance.nodeCellSize;
        musicSpeed = GameManager.instance.playSpeed;
        nodes = GameManager.instance.nodes;
        //reset content pos
        startY = -windowsRect.sizeDelta.y / 2;
        SetToDefault();
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
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        GameManager.instance.finalPuzzle.SetActive(false);
        GameManager.instance.FreezeOrUnfreeze();
        GameManager.instance.puzzleSolved = true;
        //do purify
        Purify();
    }

    void Purify()
    {
        //waiting to be implemented
    }

    void StartPuzzle()
    {
        
        if (puzzleStart)
        {
            rectTransform.anchoredPosition += new Vector2(0, musicSpeed * Time.fixedDeltaTime);
            //play audio if it is filled
            distanceMoved += musicSpeed * Time.fixedDeltaTime;
            
            if (distanceMoved > (index * 2 + 1) * windowsRect.sizeDelta.y / gridSizePerPage.y / 2)
            {
                if (nodes[index].isFilled)
                {
                    audioSource.clip = nodes[index].audioClip;
                    audioSource.Play();
                    index++;
                }
                else
                {
                    musicSpeed = 0f;
                    CheckCorrectness();
                }
            }
        }
    }



    void CheckCorrectness()
    {
        if (instrument.pitch > 0)
        {
            Debug.Log(instrument.pitch);
            Debug.Log(nodes[index].pitch);
            if (instrument.pitch == nodes[index].pitch)
            {
                audioSource.clip = nodes[index].audioClip;
                audioSource.Play();
                musicSpeed = GameManager.instance.playSpeed;
                index++;
            }
            else
            {
                puzzleStart = false;
                StartCoroutine(ShowWrongEffect(index));
            }

        }
    }

    void SetToDefault()
    {
        index = 0;
        GameManager.instance.finalPuzzle.SetActive(false);
        rectTransform.sizeDelta = new Vector2(0, nodes.Count * (windowsRect.sizeDelta.y / gridSizePerPage.y));
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY);
        musicSpeed = GameManager.instance.playSpeed;
        distanceMoved = 0;
        GameManager.instance.FreezeOrUnfreeze();
    }

    IEnumerator ShowWrongEffect(int index)
    {
        for (int i = 0; i < 6; i++)
        {
            if (transform.GetChild(index).GetComponent<Image>().color == Color.red)
            {
                transform.GetChild(index).GetComponent<Image>().color = Color.white;
            }
            else
            {
                transform.GetChild(index).GetComponent<Image>().color = Color.red;
            }
            yield return new WaitForSeconds(0.3f);
        }
        SetToDefault();
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
            node.AddComponent<BoxCollider2D>();
            nodes[i].audioClip = instrument.instrumentNotes[nodes[i].pitch - 1];
            if (nodes[i].isFilled)
            {
                image.color = Color.green;
            }
        }

    }
}


[System.Serializable]
public class Node
{
    public bool isFilled; //whether this node is going to be filled by player
    public int pitch;
    public Sprite image;
    [HideInInspector]public AudioClip audioClip;
}