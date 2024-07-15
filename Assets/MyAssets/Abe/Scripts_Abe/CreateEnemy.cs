using System.Collections;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    [SerializeField] GameObject asteroid, bunny0,bunny1,arrow;
    float offsetTime = 5;
    void Start()
    {
        StartCoroutine("Create");
    }

    IEnumerator Create()
    {
        Vector3 cPos;
        yield return new WaitForSeconds(offsetTime);
        int r  = UnityEngine.Random.Range(0, 3);
        if (r == 0)
        {
            cPos = transform.position + new Vector3(UnityEngine.Random.Range(-15f, 15f), 0, 0);
            Rigidbody2D rb = Instantiate(asteroid, cPos, Quaternion.Euler(-cPos)).GetComponent<Rigidbody2D>();
            rb.AddForce(-cPos, ForceMode2D.Impulse);
        }
        else if (r == 1)
        {
            int r1 = UnityEngine.Random.Range(0, 2);
            if (r1 == 0)
            {
                cPos = transform.position + new Vector3(UnityEngine.Random.Range(-15f, 15f), 0, 0);
                Rigidbody2D rb = Instantiate(bunny0, cPos, Quaternion.Euler(-cPos)).GetComponent<Rigidbody2D>();
                rb.AddForce(-cPos, ForceMode2D.Impulse);
            }
            if (r1 == 1)
            {
                cPos = transform.position + new Vector3(UnityEngine.Random.Range(-15f, 15f), 0, 0);
                Rigidbody2D rb = Instantiate(bunny1, cPos, Quaternion.Euler(-cPos)).GetComponent<Rigidbody2D>();
                rb.AddForce(-cPos, ForceMode2D.Impulse);
            }

        }
        else if (r == 2)
        {
            Instantiate(arrow, transform.position + new Vector3(UnityEngine.Random.Range(-2f, 2f), 0,0),Quaternion.identity);
        }

        StartCoroutine("Create");
    }

}
