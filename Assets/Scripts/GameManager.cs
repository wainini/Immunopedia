using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    
    [SerializeField] private int lives;
    
    public GameObject neutrofilPrefab;
    public GameObject enemyPrefab;

    public Queue<GameObject> wounds = new Queue<GameObject>();

    public Transform initialEnemySpawn;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        var woundArr = GameObject.FindGameObjectsWithTag("Wound");
        wounds = new Queue<GameObject>(woundArr);
        Debug.Log("You have " + lives + " lives");
    }

    void Update()
    {
        if(wounds.Count != 0)
        {
            if (wounds.Peek().GetComponent<Wound>().IsWoundClosed())
            {
                wounds.Dequeue();
            }
        }
        else
        {
            Win();
        }
    }

    public void ReduceLive()
    {
        lives--;
        if (lives <= 0) GameOver();
    }

    private void GameOver()
    {
        //game over
        Debug.Log("You Died");
        Time.timeScale = 0;
    }

    private void Win()
    {
        //win
        Debug.Log("You Win");
        Time.timeScale = 0;
    }
}
