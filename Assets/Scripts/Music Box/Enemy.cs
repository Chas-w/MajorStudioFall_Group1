using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    Collider cld;
    //public GameObject spawnArea;
    [HideInInspector]public Transform spawnPos;
    public GameObject toy;
    [HideInInspector] public int pitch;
    [HideInInspector] public AudioClip clip;
    Rigidbody rb;
    NavMeshAgent navA;
    public bool canMove;
    public MusicBoxManager mbManager; 
    float speed;
    float attackRange;
    [SerializeField] AudioSource audioSource;

    [Header("Match Data")]
    public string songNotes;

    private void Awake()
    {
        cld = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        navA = GetComponent<NavMeshAgent>();
        //audioSource = GetComponent<AudioSource>();
        //spawnCld = spawnArea.GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume = 50;
        speed = GameManager.instance.enemySpeed;
        attackRange = GameManager.instance.enemyAttackRange;
        navA.speed = speed;
        navA.stoppingDistance = attackRange;
        StartCoroutine(PlayNode());
    }

    // Update is called once per frame
    void Update()
    {
        //CheckAttackRange();
        //MoveTowardPlayer();
        MoveTowardToy();
        CheckPlayerNode();
    }

    void CheckPlayerNode()
    {
        Vector3 playerHorPos = new Vector3(GameManager.instance.player.transform.position.x, 0, GameManager.instance.player.transform.position.z);
        Vector3 enemyHorPos = new Vector3(transform.position.x, 0, transform.position.z);
        float distance = Vector3.Distance(playerHorPos, enemyHorPos);

        if (distance <= GameManager.instance.killableRange)
        {
            if (GameManager.instance.instrument.notesPlayed.Contains(songNotes))
            {
                Spawn();
            }
        }
    }

    IEnumerator PlayNode()
    {
        while (true)
        {
            //audioSource.Play();
            Debug.Log(int.Parse(songNotes[0].ToString()));
            clip = GameManager.instance.instrument.instrumentNotes[int.Parse(songNotes[0].ToString()) - 1];
           
            audioSource.PlayOneShot(clip);
            if (songNotes.Length > 1)
            {
                yield return new WaitForSeconds(0.5f);
                clip = GameManager.instance.instrument.instrumentNotes[int.Parse(songNotes[1].ToString()) - 1];
                Debug.Log(int.Parse(songNotes[1].ToString()));
                audioSource.PlayOneShot(clip);
            }
            if (songNotes.Length > 2)
            {
                yield return new WaitForSeconds(0.5f);
                clip = GameManager.instance.instrument.instrumentNotes[int.Parse(songNotes[2].ToString()) - 1];
                Debug.Log(int.Parse(songNotes[2].ToString()));
                audioSource.PlayOneShot(clip);
            }
            yield return new WaitForSeconds (GameManager.instance.playbackInterval);
            //audioSource.Stop();
        }
    }

    public void Spawn()
    {
        Vector3 pos = GetRandomPointInCircle(spawnPos.position, GameManager.instance.spawnRange);
        //pos += new Vector3(0, transform.localScale.y / 2, 0);

        /*
        float maxDistance = 10.0f;  // Set based on the expected distance from the agent to the NavMesh

        NavMeshHit hit;

        if (NavMesh.SamplePosition(pos, out hit, maxDistance, NavMesh.AllAreas))
        {
            pos = hit.position;  // Adjust spawn position to the nearest point on the NavMesh
        }
        */
        int twoOrThree = UnityEngine.Random.Range(2, 4);
        if (twoOrThree == 2)
        {
            songNotes = UnityEngine.Random.Range(1, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString();
        } else
        {
            songNotes = UnityEngine.Random.Range(1, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString();
        }

        navA.Warp(pos);
        transform.position = pos;
    }

    Vector3 GetRandomPointInCircle(Vector3 pos, float radius)
    {
        // Generate a random angle in radians
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);

        // Generate a random radius, scaled to the square root for uniform distribution
        float distance = Mathf.Sqrt(UnityEngine.Random.Range(0f, 1f)) * radius;

        // Calculate the random point using polar coordinates
        float x = pos.x + Mathf.Cos(angle) * distance;
        float z = pos.z + Mathf.Sin(angle) * distance;

        return new Vector3(x, pos.y, z);
    }

    void MoveTowardPlayer()
    {
        if (canMove && !mbManager.boxHealed && !GameManager.instance.freeze) //if the music box is also not healed yet (the player hasn't finished the puzzle)
        {
            navA.isStopped = false;
            Vector3 destination = GameManager.instance.player.transform.position;
            navA.destination = destination;
            /*
            Vector3 dir = GameManager.instance.player.transform.position - transform.position;
            dir.Normalize();
            dir = new Vector3(dir.x, 0, dir.z);
            rb.AddForce(dir * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            */
        } else
        {
            navA.isStopped = true;
        }
    }

    void MoveTowardToy()
    {
        if (!mbManager.boxHealed && !GameManager.instance.freeze) //if the music box is also not healed yet (the player hasn't finished the puzzle)
        {
            navA.isStopped = false;
            Vector3 destination = toy.transform.position;
            navA.destination = destination;
        }
        else
        {
            navA.isStopped = true;
        }
        CheckAttackRange();
    }

    void CheckAttackRange()
    {
        Vector3 toyHosPos = new Vector3(toy.transform.position.x, 0, toy.transform.position.z);
        Vector3 enemyHorPos = new Vector3(transform.position.x, 0, transform.position.z);
        float distance = Vector3.Distance(toyHosPos, enemyHorPos);
        
        if (distance <= attackRange)
        {
            GameManager.instance.toyList.Remove(toy);
            Destroy(toy);
            Destroy(gameObject);
        }
    }
}

[Serializable]
public class EnemyInfo
{
    public Transform spawnPos;
    public GameObject toy;
}
