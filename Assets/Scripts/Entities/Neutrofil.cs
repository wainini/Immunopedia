using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrofil : MonoBehaviour
{
    public ImmuneCell cellData;

    private void Update()
    {
        cellData.WaitForInterval();
        if (cellData.target)
        {
            transform.position = Vector2.MoveTowards(transform.position, cellData.GetTargetLocation().position, cellData.movSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, cellData.GetTargetLocation().position) < 0.5)
            {
                cellData.SetTarget();
                transform.position = this.transform.position;
                if (cellData.IsReadyToAttack())
                {
                    //Attack(enemyObj);
                    cellData.Attack();
                    cellData.RestoreInterval();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            cellData.AddEnemy(collision.gameObject.transform);
        }
    }
}
