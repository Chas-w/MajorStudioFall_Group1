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
    public int enemyNum;
    public List<Enemy> enemyList;
    public float enemySpeed;
    public float enemyAttackRange;
    public float searchTime;
    float timer;
    

    [Header("Instrument Status")]
    public bool instrumentIsPlaying;

    [Header("Player Settings")]
    public GameObject player;
    public float horizontalSensitivity;
    public float verticalSensitivity;

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
        //SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    
    void SpawnEnemy()
    {
        GameObject enemyParent = new GameObject("EnemyParent");
        for (int i = 0; i < enemyNum; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemyParent.transform);
            enemyList.Add(enemy.GetComponent<Enemy>());
            enemy.transform.position = new Vector3(Random.Range(-floorCld.bounds.extents.x, floorCld.bounds.extents.x), 0, Random.Range(-floorCld.bounds.extents.z, floorCld.bounds.extents.z));
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
            for (int i = 0; i < enemyNum; i++)
            {
                enemyList[i].canMove = true;
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = searchTime;
                instrumentIsPlaying = false;
            }

        } else
        {
            for (int i = 0; i < enemyNum; i++)
            {
                enemyList[i].canMove = false;
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
