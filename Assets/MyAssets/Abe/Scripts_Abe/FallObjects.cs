using SO;
using UnityEngine;

namespace main
{
    public class FallObjects : MonoBehaviour
    {
        SO_Tags tags;
        Rigidbody2D myRB;
        float power = 1000;
        bool doOnce = false;
        void Awake()
        {
            myRB = GetComponent<Rigidbody2D>();
            tags = SO_Tags.Entity;
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == tags.ItemTag && !doOnce)
            {
                Rigidbody2D hitObjRB = collision.gameObject.GetComponent<Rigidbody2D>();
                doOnce = true;
                //hitObjRB.AddForce(myRB.velocity.normalized * power);
                hitObjRB.AddForce(Random.insideUnitSphere* power);
                //Destroy(gameObject);
            }
        }
    }
}