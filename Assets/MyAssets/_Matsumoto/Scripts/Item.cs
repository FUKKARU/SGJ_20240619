using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

namespace Main
{
    public class Item : MonoBehaviour
    {
        [Header("重力")]
        [SerializeField] float gravity;

        [Header("抗力")]
        [SerializeField] float dragValue;

        bool isFall;
        [NonSerialized] public bool isGround;
        Rigidbody2D rb;
        Collider2D col;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();

            // デフォルト重力off
            rb.gravityScale = 0;
            rb.drag = dragValue;
            col.enabled = false;
            isGround = false;
            isFall = false;
        }

        private void FixedUpdate()
        {
            // 落下フラグがオンなら
            if (isFall)
            {
                // 重力
                rb.AddForce(Vector3.down * Mathf.Abs(gravity), ForceMode2D.Force);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 落ち物衝突
            if (collision.gameObject.CompareTag("Item"))
            {
                Item item = collision.gameObject.GetComponent<Item>();

                // 衝突した落ち物が接地していれば
                if (item.isGround)
                {
                    // 落下済みにさせる
                    isGround = true;

                    // カメラを追従させる
                    PlayerCamera playerCamera = Camera.main.GetComponent<PlayerCamera>();
                    playerCamera.CreateNewCameraPosition();
                }
            }

            // 富士山衝突
            else
            {
                // 落下済みにさせる
                isGround = true;

                // カメラを追従させる
                PlayerCamera playerCamera = Camera.main.GetComponent<PlayerCamera>();
                playerCamera.CreateNewCameraPosition();
            }
        }

        // 落下させる
        public void Set()
        {
            isFall = true;
            transform.parent = null;
            col.enabled = true;
        }

        // 落下済み かつ 接地済み か
        public bool IsActive()
        {
            return isFall && isGround;
        }
    }
}


