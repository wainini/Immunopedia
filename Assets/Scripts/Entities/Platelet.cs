using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platelet : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    
    private Transform woundPosition;
    private bool isClosingWound = false;

    private void Update()
    {
        if (!woundPosition)
        {
            woundPosition = GameManager.instance.wounds.Peek().transform;
        }
        if (!isClosingWound)
        {        
            transform.position = Vector2.MoveTowards(transform.position, woundPosition.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wound")
        {
            //initialize close wound
            transform.position = this.transform.position;
            //Debug.Log("Closing wound");
        }
    }
}
