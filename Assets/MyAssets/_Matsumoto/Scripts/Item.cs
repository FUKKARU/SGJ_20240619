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
        [Header("�d��")]
        [SerializeField] float gravity;

        [Header("�R��")]
        [SerializeField] float dragValue;

        bool isFall;
        [NonSerialized] public bool isGround;
        [NonSerialized] public int id = -1;
        Rigidbody2D rb;
        Collider2D col;

        // ���g�ɕt�^����AudioSource���L���b�V�����Ă���
        private AudioSource _source = null;

        // �ŏ��ɐڐG�����ۂ�true�ɂȂ�A�ȍ~��false�ɂȂ邱�Ƃ͂Ȃ�
        private bool _isHit = false;

        private void Start()
        {
            // AudioSource��t�^�A�L���b�V��
            _source = gameObject.AddComponent<AudioSource>();

            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();

            // �f�t�H���g�d��off
            rb.gravityScale = 0;
            rb.drag = 0;
            col.enabled = false;
            isGround = false;
            isFall = false;
        }

        private void Update()
        {
            // �������Ă��Ȃ����ڒn���Ă��Ȃ��Ȃ�
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

            if (!_isHit)
            {
                // ����ŁA���ケ�̃X�R�[�v�̏�����2�x�ƌĂ΂�Ȃ��Ȃ�
                _isHit = true;

                // �ŏ��ɐڐG�����ۂɁASE���Đ�
                PlaySE();
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

        // ���̃C���X�^���X�̎�ނɉ�����SE���Đ�����
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
                        _ => throw new Exception("�����Ȏ�ނł�")
                    }
                    , SType.SE
                );
        }
    }
}


