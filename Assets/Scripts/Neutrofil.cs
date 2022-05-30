using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrofil : MonoBehaviour
{
    public ImmuneCell immuneCell;
    private double health;
    private double atk;

    private List<Transform> enemies;

    private void Start()
    {
        //Debug.Log(this.immuneCell.health + " " + this.immuneCell.atk);
        //health = immuneCell.health;
        //atk = immuneCell.atk;
        enemies = new List<Transform>();
    }

    private void Update()
    {
        if(enemies.Count != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemies[0].position, immuneCell.movSpd * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Transform enemyLocation = collision.gameObject.transform;
            enemies.Add(enemyLocation);
            Debug.Log(enemies[0].position);
        }
    }
}
