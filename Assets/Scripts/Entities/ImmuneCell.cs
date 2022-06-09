using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Immune Cell", menuName = "Entity/Immune Cell")]
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

    public void SetTarget()
    {
        target = (enemies.Count != 0) ? enemies.Peek().GetComponent<Entity>() : null;
    }

    public Transform GetTargetLocation()
    {
        return (enemies.Count != 0) ? enemies.Peek() : null; ;
    }
}
