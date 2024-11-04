using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Collider floorCld;
    public bool freeze;

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
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        //EnemyMovement();
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
