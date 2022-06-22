using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityBehaviour
{
    public void Attack();
    public bool IsDead();
    public bool IsReadyToAttack();
    public void WaitForInterval();
    public void RestoreInterval();
}
