using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Entity/Immune Cell", menuName ="New Immune Cell")]
public class ImmuneCell : Entity
{
    [SerializeField] private float atkRadius;
    [SerializeField] private CircleCollider2D radius;

    public Queue<Transform> enemies = new Queue<Transform>();

    public ImmuneCell()
    {
        radius.radius = atkRadius;
    }

    public void AddEnemy(Transform enemy)
    {
        enemies.Enqueue(enemy);
    }

    public Transform GetTargetLocation()
    {
        if(enemies.Count != 0)
        {
            return enemies.Peek();
        }
        else
        {
            return null;
        }
    }
}
