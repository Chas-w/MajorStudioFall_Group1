using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Collider floorCld;

    [Header("Enemy Settings")]
    public GameObject enemyParent;
    public GameObject enemyPrefab;
    public int enemyNum;
    public List<Enemy> enemyList;
    public float enemySpeed;
    public float enemyMaxSpeed;
    public float enemyAttackRange;

    [Header("Instrument Status")]
    public bool instrumentIsPlaying;
    public float timer = 2f;//this needs to be changed based on the audio length

    [Header("Player Settings")]
    public GameObject player;

    private void Awake()
    {
        instance = this;
        //InitializeEnemyList();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        SpawnEnemy();
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
        } else
        {
            for (int i = 0; i < enemyNum; i++)
            {
                enemyList[i].canMove = false;
            }
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            timer = 2f;
            instrumentIsPlaying = false;
        }
    }
}
