using SO;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace main
{
    public class FallObjects : MonoBehaviour
    {
        SO_Tags tags;
        Rigidbody2D myRB;
        float power = 2;
        void Awake()
        {
            myRB = GetComponent<Rigidbody2D>();
            tags = SO_Tags.Entity;
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == tags.ItemTag)
            {
                Rigidbody2D hitObjRB = collision.gameObject.GetComponent<Rigidbody2D>();
                hitObjRB.AddForce(myRB.velocity.normalized * power);
                //Destroy(gameObject);
            }
        }
    }
}