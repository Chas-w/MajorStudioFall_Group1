using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaManager : MonoBehaviour
{
    //Logistic Objects
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject enemyParent;
    [SerializeField] GameObject player;


    //Toys
    [SerializeField] GameObject crow;
    [SerializeField] GameObject dolls;
    [SerializeField] GameObject teddy;

    //Toy Final Position
    [SerializeField] Transform crowFinal;
    [SerializeField] Transform teddyFinal;
    [SerializeField] Transform dollFinal;

    //End Game Position
    Transform crowEnd;
    Transform teddyEnd;
    Transform dollEnd;
    Transform playerEnd;

    //private variables
    bool isStart;


    // Start is called before the first frame update
    void Start()
    {
        gameManager.SetActive(false);
        enemyParent.SetActive(false);

        gameManager.GetComponent<GameManager>().freeze = true;

        crowEnd = crow.transform;
        teddyEnd = teddy.transform;
        dollEnd = dolls.transform;
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
        gameManager.GetComponent<GameManager>().freeze = false;
    }

    public void EndGame()
    {
        if(dolls != null)
        {
            dolls.transform.position = dollEnd.position;
            dolls.transform.rotation = dollEnd.rotation;
        }

        if(teddy != null)
        {
            teddy.transform.position = teddyEnd.position;
            teddy.transform.rotation = teddyEnd.rotation;
        }

        if(crow != null)
        {
            crow.transform.position = crowEnd.position;
            crow.transform.rotation = crowEnd.rotation;
        }

        player.transform.position = playerEnd.position;
        player.transform.rotation = playerEnd.rotation;

        gameManager.GetComponent<GameManager>().freeze = true;
    }

}
