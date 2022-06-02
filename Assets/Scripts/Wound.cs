using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wound : MonoBehaviour
{
    [SerializeField] private int plateletNeeded;

    private int currentPlateletCount;

    private bool isWoundClosed;
    private void Start()
    {
        currentPlateletCount = 0;
    }

    private void Update()
    {
        if(currentPlateletCount >= plateletNeeded)
        {
            isWoundClosed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Platelet")
        {
            Debug.Log("Closing wound");
            currentPlateletCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Platelet")
        {
            currentPlateletCount++;
        }
    }

    public bool IsWoundClosed()
    {
        return isWoundClosed;
    }
}
