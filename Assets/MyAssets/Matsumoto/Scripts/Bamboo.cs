using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo : MonoBehaviour
{
    [Header("�d��")]
    [SerializeField] float gravity;

    bool isFall;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �f�t�H���g�d��off
        rb.gravityScale = 0;
        isFall = false;
    }

    private void FixedUpdate()
    {
        // �d��
        rb.AddForce(Vector3.down * Mathf.Abs(gravity), ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFall)
        {
            CameraMov cameraMov = Camera.main.GetComponent<CameraMov>();
            cameraMov.CreateNewCameraPosition();
            isFall = true;
        }
    }
}
