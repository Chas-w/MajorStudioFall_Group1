using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject endScreen;
    public GameObject tryAgain;


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
    bool canTryAgain;

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

        endScreen.SetActive(false);
        tryAgain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        NightTimeCountDown();
        CheckToyCount();
        //EnemyMovement();

        if(canTryAgain && Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void CheckToyCount()
    {
        toyRemainText.text = "Toys Remaining: " + toyList.Count.ToString();
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
            nightTimerText.text = "Night Time Remaining: " + Mathf.Round(nightTimer).ToString() + "s";
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
        canTryAgain = true;
        switch (toyList.Count)
        {
            case 0:
                resultText.text = "I’m sorry I couldn't save you."; //waiting to be modified
                endScreen.SetActive(true);
                finalScreen.SetActive(false);
                break;
            case 1:
                resultText.text = "Why did you let my friends die?";//waiting to be modified
                tryAgain.SetActive(true);
                break;
            case 2:
                resultText.text = "Thank you. I guess. ";//waiting to be modified
                tryAgain.SetActive(true);
                break;
            case 3:
                resultText.text = "Thank you. Thank you. Thank you! You saved us! Those meanies will burn in hell.";//waiting to be modified
                tryAgain.SetActive(true);
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
