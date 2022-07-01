using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platelet : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 3f;
    
    private Transform woundPosition;
    private bool isClosingWound = false;

    private void Update()
    {
        woundPosition = GameManager.instance.wounds.Peek().transform;
        if (!isClosingWound)
        {
            anim.SetBool("IsMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, woundPosition.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wound")
        {
            anim.SetBool("IsHealing", true);
            //initialize close wound
            transform.position = this.transform.position;
            isClosingWound = true;
        }
    }

    public void HealComplete()
    {
        anim.SetBool("IsHealing", false);
        woundPosition.gameObject.GetComponent<Wound>().AddPlatelet();
        Destroy(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isClosingWound = false;
    }
}
