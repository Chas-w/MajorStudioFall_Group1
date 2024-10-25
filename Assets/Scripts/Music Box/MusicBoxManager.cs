using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxManager : MonoBehaviour
{
    [Header("External Data")]
    public GameObject gameManagerObject;
    public GameManager gm;
    public Enemy enemy;

    [Header("Internal Data")]
    public bool boxHealed; 
    // Start is called before the first frame update
    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager"); 
    }

    // Update is called once per frame
    void Update()
    {
        gm = gameManagerObject.GetComponent<GameManager>();

        if (gm.correctCombination) //when the puzzle is solved the enemy will freeze - in further iterations maybe more can happen
        {
            enemy.canMove = false;
            boxHealed = true; 
        }
    }
}
