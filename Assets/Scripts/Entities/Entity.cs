using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Foreign Substance", menuName = "Entity/Foreign Substance")]
public class Entity : ScriptableObject
{
    public int maxHealth;
    public int atk;
    public int defense;
    public float movSpeed;
    public float atkInterval;
    public int blockCount;
}
