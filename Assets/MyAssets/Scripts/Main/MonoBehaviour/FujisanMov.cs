using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FujisanMov : MonoBehaviour
{
    SO_Tags tags;
    void Awake()
    {
        tags = SO_Tags.Entity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitO = collision.gameObject;
        if(hitO.tag == tags.BambooTag)
        {
            hitO.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            hitO.GetComponent<Bamboo_FallController>().collisionWithMtFuji = true;

        }
    }
}