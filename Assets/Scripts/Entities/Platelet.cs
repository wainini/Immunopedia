using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platelet : MonoBehaviour
{
    [SerializeField] private Transform woundPosition;
    [SerializeField] private float speed = 3f;

    private bool isClosingWound = false;

    private void Start()
    {
        woundPosition = GameObject.Find("Wound").transform;
    }

    private void Update()
    {
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
