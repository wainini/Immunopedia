using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platelet : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 3f;
    [SerializeField] private GameObject woundPrefab;
    private List<GameObject> wounds;
    
    
    private Transform woundPosition;
    private float closestDistance;
    private bool isClosingWound = false;

    [HideInInspector] public CellTrainingData trainData;
    [HideInInspector] public string key = "PlateletUpLvl";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 0);
        }
        wounds = GameManager.instance.wounds;
    }
    private void Update()
    {
        closestDistance = float.PositiveInfinity;
        foreach(GameObject wound in wounds)
        {
            float tempDistance = Vector2.Distance(this.transform.position, wound.transform.position);
            if (tempDistance < closestDistance)
            {
                closestDistance = tempDistance;
                woundPosition = wound.transform;
            }
        }
        if(woundPosition == null)
        {
            return;
        }
        if (!isClosingWound)
        {
            anim.SetBool("IsMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, woundPosition.position, speed * Time.deltaTime);
            if (transform.position.x > woundPosition.transform.position.x)
            {
                transform.rotation = Quaternion.identity;
            }
            else if (transform.position.x < woundPosition.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
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

    public void Upgrade()
    {
        int upgradeLevel = PlayerPrefs.GetInt(key);
        if (upgradeLevel < 3)
        {
            trainData.cost--;
        }
        else
        {
            SpecialUpgrade();
        }
        PlayerPrefs.SetInt(key, ++upgradeLevel);
    }

    void SpecialUpgrade()
    {
        Debug.Log("Special Upgrade");
        woundPrefab.GetComponent<Wound>().plateletNeeded--;
        //stats.atkUp += 30;
        //stats.blockCount++;
    }

    public void Revert()
    {
        woundPrefab.GetComponent<Wound>().plateletNeeded++;
    }
}
