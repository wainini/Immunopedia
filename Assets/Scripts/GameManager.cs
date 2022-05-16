using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(prefab);
        }
    }
}
