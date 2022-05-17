using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrofil : MonoBehaviour
{
    public ImmuneCell immuneCell;
    public double health;
    public double atk;

    private List<Transform> enemies = new List<Transform>();

    private void Start()
    {
        //Debug.Log(this.immuneCell.health + " " + this.immuneCell.atk);
        //health = immuneCell.health;
        //atk = immuneCell.atk;
        enemies = null;
    }

    private void Update()
    {
        if(enemies != null)
        {
            Vector2.MoveTowards(transform.position, enemies[0].position, immuneCell.movSpd * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Transform enemyLocation = collision.gameObject.transform;
            enemies.Add(enemyLocation);
        }
    }
}
