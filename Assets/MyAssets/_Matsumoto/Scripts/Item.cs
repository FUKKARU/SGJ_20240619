using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

namespace Main
{
    public class Item : MonoBehaviour
    {
        [Header("�d��")]
        [SerializeField] float gravity;

        [Header("�R��")]
        [SerializeField] float dragValue;

        bool isFall;
        [NonSerialized] public bool isGround;
        Rigidbody2D rb;
        Collider2D col;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();

            // �f�t�H���g�d��off
            rb.gravityScale = 0;
            rb.drag = dragValue;
            col.enabled = false;
            isGround = false;
            isFall = false;
        }

        private void FixedUpdate()
        {
            // �����t���O���I���Ȃ�
            if (isFall)
            {
                // �d��
                rb.AddForce(Vector3.down * Mathf.Abs(gravity), ForceMode2D.Force);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // �������Փ�
            if (collision.gameObject.CompareTag("Item"))
            {
                Item item = collision.gameObject.GetComponent<Item>();

                // �Փ˂������������ڒn���Ă����
                if (item.isGround)
                {
                    // �����ς݂ɂ�����
                    isGround = true;

                    // �J������Ǐ]������
                    PlayerCamera playerCamera = Camera.main.GetComponent<PlayerCamera>();
                    playerCamera.CreateNewCameraPosition();
                }
            }

            // �x�m�R�Փ�
            else
            {
                // �����ς݂ɂ�����
                isGround = true;

                // �J������Ǐ]������
                PlayerCamera playerCamera = Camera.main.GetComponent<PlayerCamera>();
                playerCamera.CreateNewCameraPosition();
            }
        }

        // ����������
        public void Set()
        {
            isFall = true;
            transform.parent = null;
            col.enabled = true;
        }

        // �����ς� ���� �ڒn�ς� ��
        public bool IsActive()
        {
            return isFall && isGround;
        }
    }
}


