using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaManager : MonoBehaviour
{
    //Logistic Objects
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject enemyParent;


    //Toys
    [SerializeField] GameObject crow;
    [SerializeField] GameObject dolls;
    [SerializeField] GameObject teddy;

    //Toy Final Position
    [SerializeField] Transform crowFinal;
    [SerializeField] Transform teddyFinal;
    [SerializeField] Transform dollFinal;

    //private variables
    bool isStart;


    // Start is called before the first frame update
    void Start()
    {
        gameManager.SetActive(false);
        enemyParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStart && dialogue.GetComponent<DialogueManager>().contentIndex == 11 && Input.GetKey(KeyCode.Space))
        {
            StartGame();
            isStart = true;
        }
    }



    private void StartGame()
    {
        crow.transform.position = crowFinal.position;
        crow.transform.rotation = crowFinal.rotation;

        dolls.transform.position = dollFinal.position;
        dolls.transform.rotation = dollFinal.rotation;

        teddy.transform.position = teddyFinal.position;
        teddy.transform.rotation = teddyFinal.rotation;


        gameManager.SetActive(true);
        enemyParent.SetActive(true);
    }

}
