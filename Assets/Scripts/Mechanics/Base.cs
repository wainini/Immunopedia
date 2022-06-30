using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.transform.parent.gameObject);
            GameManager.instance.ReduceLive(collision.gameObject.GetComponent<EntityStats>().hpSealReduction);
        }
    }
}
