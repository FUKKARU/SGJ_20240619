using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo_FallController : MonoBehaviour
{
    public bool collisionWithMtFuji = false;

    SO_Tags tags;
    void Awake()
    {
        tags = SO_Tags.Entity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitO = collision.gameObject;
        if (hitO.tag == tags.BambooTag && collisionWithMtFuji)
        {
            hitO.GetComponent<Bamboo_FallController>().collisionWithMtFuji = true;

        }
    }
}
