using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrofil : MonoBehaviour
{
    public ImmuneCell cellData;

    private void Update()
    {
        cellData.WaitForInterval();
        if (cellData.GetTargetLocation())
        {
            transform.position = Vector2.MoveTowards(transform.position, cellData.GetTargetLocation().position, cellData.movSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, cellData.GetTargetLocation().position) < 0.5)
            {
                cellData.SetTarget(cellData.GetTargetLocation().GetComponent<Entity>());
                transform.position = this.transform.position;
                //Enemy enemyObj = enemies.Peek().GetComponent<Enemy>();
                //enemyObj.SetTarget(this.transform);
                if (cellData.IsReadyToAttack())
                {
                    //Attack(enemyObj);
                    cellData.Attack();
                    cellData.RestoreInterval();
                }
            }
        }
        //if(cellData.enemies.Count != 0)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, cellData.enemies.Peek().position, cellData.movSpeed * Time.deltaTime);
        //    if(Vector2.Distance(transform.position, cellData.enemies.Peek().position) < 0.5)
        //    {
        //        cellData.SetTarget(cellData.enemies.Peek().GetComponent<Entity>());
        //        transform.position = this.transform.position;
        //        Enemy enemyObj = enemies.Peek().GetComponent<Enemy>();
        //        enemyObj.SetTarget(this.transform);
        //        if(cellData.IsReadyToAttack())
        //        {
        //            Attack(enemyObj);
        //            cellData.RestoreInterval();
        //        }
        //    }
        //    if(enemies.Peek() == null)
        //    {
        //        enemies.Dequeue();
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            cellData.AddEnemy(collision.gameObject.transform);
        }
    }

    //private void Attack(Enemy enemy)
    //{
    //    enemy.TakeDamage(immuneCell.atk);
    //    if (enemy.IsDead())
    //    {
    //        enemies.Dequeue();
    //    }
    //}

    //public void TakeDamage(int damage)
    //{
    //    currentHealth -= damage;
    //    if(currentHealth <= 0)
    //    {
    //        currentHealth = 0;
    //        Destroy(this.gameObject);
    //    }
    //    healthFill.size = new Vector2(currentHealth * healthBar.size.x / immuneCell.health, healthBar.size.y);
    //}
}
