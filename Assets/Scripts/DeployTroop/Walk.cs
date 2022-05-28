using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right;
    }

    IEnumerator IWantToKillMySelf()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
