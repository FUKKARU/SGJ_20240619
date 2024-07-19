using System.Collections;
using System.Collections.Generic;
using Main;
using UnityEngine;

public class Glue : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Camera.main.transform.position.y > transform.position.y)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
