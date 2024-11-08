using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Collider floorCld;
    public bool freeze;

    [Header("Game Status")]
    public float nightDuration;
    public GameObject finalScreen;
    public TextMeshProUGUI nightTimerText;
    public TextMeshProUGUI toyRemainText;
    public TextMeshProUGUI resultText;
    private float nightTimer;
    public GameObject cinemaManager;


    [Header("Enemy Settings")]
    public GameObject enemyParent;
    public GameObject enemyPrefab;
    public List<EnemyInfo> enemyList;
    public float enemySpeed;
    public float enemyAttackRange;
    public float searchTime;
    public float timer;
    public float playbackInterval;
    public float killableRange;
    public float spawnRange;


    [Header("Instrument Status")]
    public bool instrumentIsPlaying;
    public InstrumentPlayer instrument;

    [Header("Player Settings")]
    public GameObject player;
    public float horizontalSensitivity;
    public float verticalSensitivity;
    public List<GameObject> toyList;
    //public GameObject currentHoldingToy;

    [Header("Puzzle Status")]
    public bool puzzleSolved = false;
    public GameObject finalPuzzle;
    public float playSpeed;
    public Vector2 gridSizePerPage;
    public float nodeCellSize;
    public List<Node> nodes = new List<Node>();
    public bool correctCombination; 


    private void Awake()
    {
        instance = this;
        //InitializeEnemyList();
    }

    // Start is called before the first frame update
    void Start()
    {
        freeze = false;
        nightTimer = nightDuration;
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        NightTimeCountDown();
        CheckToyCount();
        //EnemyMovement();
    }

    void CheckToyCount()
    {
        toyRemainText.text = "Toy Remain: " + toyList.Count.ToString();
        if (toyList.Count == 0)
        {
            GameEnd();
        }
    }

    void NightTimeCountDown()
    {
        if (nightTimer > 0)
        {
            nightTimer -= Time.deltaTime;
            nightTimerText.text = "Night Time Remain: " + Mathf.Round(nightTimer).ToString() + "s";
        } else
        {
            GameEnd();
        }
    }

    void GameEnd()
    {
        //freeze = true;
        nightTimerText.gameObject.SetActive(false);
        toyRemainText.gameObject.SetActive(false);
        finalScreen.GetComponent<CanvasGroup>().alpha += 0.01f;
        cinemaManager.GetComponent<CinemaManager>().EndGame();
        switch (toyList.Count)
        {
            case 0:
                resultText.text = "I’m sorry."; //waiting to be modified
                break;
            case 1:
                resultText.text = "Why did you let my friends die?";//waiting to be modified
                break;
            case 2:
                resultText.text = "Thank you. I guess. ";//waiting to be modified
                break;
            case 3:
                resultText.text = "Thank you. Thank you. Thank you! You saved us! Those meanies will burn in hell.";//waiting to be modified
                break;
        }
            

    }
  
    void SpawnEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            GameObject musicBox = Instantiate(enemyPrefab, enemyParent.transform);
            Enemy enemy = musicBox.GetComponent<Enemy>();
            enemy.toy = enemyList[i].toy;
            enemy.spawnPos = enemyList[i].spawnPos;
            enemy.pitch = enemyList[i].pitch;
            enemy.clip = instrument.instrumentNotes[enemyList[i].pitch - 1];
            enemy.Spawn();
        }
    }




    /*
    void InitializeEnemyList()
    {
        enemyNum = enemyParent.transform.childCount;
        for (int i = 0; i < enemyNum; i++)
        {
            enemyList.Add(enemyParent.transform.GetChild(i).GetComponent<Enemy>());
        }
        
    }
    */

    void EnemyMovement()
    {
        if (instrumentIsPlaying)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                //enemyList[i].canMove = true;
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer += searchTime;
                instrumentIsPlaying = false;
            }

        } else
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                //enemyList[i].canMove = false;
            }
        }


    }

    public void FreezeOrUnfreeze()
    {
        //freeze player
        freeze = !freeze;
        if (freeze)
        {
            Cursor.lockState = CursorLockMode.None;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}
