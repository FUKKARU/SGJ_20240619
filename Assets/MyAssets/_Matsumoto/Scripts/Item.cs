using General;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        [NonSerialized] public int id = -1;
        Rigidbody2D rb;
        Collider2D col;

        // 自身に付与したAudioSourceをキャッシュしておく
        private AudioSource _source = null;

        // 最初に接触した際にtrueになり、以降はfalseになることはない
        private bool _isHit = false;

        private void Start()
        {
            // AudioSourceを付与、キャッシュ
            _source = gameObject.AddComponent<AudioSource>();

            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();

            // デフォルト重力off
            rb.gravityScale = 0;
            rb.drag = 0;
            col.enabled = false;
            isGround = false;
            isFall = false;
        }

        private void Update()
        {
            // 落下していないかつ接地していないなら
            if (!IsActive())
            {
                rb.drag = 0;
            }
            else
            {
                rb.drag = dragValue;
            }

            if (transform.position.y < -10)
            {
                Destroy(gameObject);
            }
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

            if (!_isHit)
            {
                // これで、今後このスコープの処理は2度と呼ばれなくなる
                _isHit = true;

                // 最初に接触した際に、SEを再生
                PlaySE();
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

        // このインスタンスの種類に応じたSEを再生する
        private void PlaySE()
        {
            _source.Raise
                (
                    id switch
                    {
                        0 => CType.Bamboo.GetClip(),
                        1 => CType.Bamboo.GetClip(),
                        2 => CType.Bamboo.GetClip(),
                        3 => CType.Bamboo.GetClip(),
                        4 => CType.Bamboo.GetClip(),
                        5 => CType.Bamboo.GetClip(),
                        6 => CType.Bamboo.GetClip(),
                        7 => CType.Bamboo.GetClip(),
                        8 => CType.Okina.GetClip(),
                        9 => CType.Mikado.GetClip(),
                        10 => CType.Ohna.GetClip(),
                        _ => throw new Exception("無効な種類です")
                    }
                    , SType.SE
                );
        }
    }
}


