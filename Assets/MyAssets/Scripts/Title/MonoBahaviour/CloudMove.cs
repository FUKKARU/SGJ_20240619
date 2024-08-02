using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace main
{
    public class CloudMove : MonoBehaviour
    {
        [SerializeField] private float cloudMoveSpeed = 1.0f; //�_�̈ړ����x
        [Header("���E���ݒ�;x=��,y=�E�̋��E��")]
        [SerializeField] private Vector2 boundaries = new Vector2(-13.0f,13.0f); //���x=��,y=�E�̋��E��
        [SerializeField] private int direction = 1; //�ړ�����(1�͉E�A-1��)

        private void Start()
        {
            
        }

        private void Update()
        {
            // x���ɉ����ăI�u�W�F�N�g���ړ�������
            transform.Translate(Vector2.right * cloudMoveSpeed * direction * Time.deltaTime);

            // �I�u�W�F�N�g���E���̋��E�𒴂�����A�����̋��E�Ɉړ�������
            if (transform.position.x > boundaries.y)
            {
                transform.position = new Vector2(boundaries.x, transform.position.y);
            }
            // �I�u�W�F�N�g�������̋��E�𒴂�����A�E���̋��E�Ɉړ�������
            else if (transform.position.x < boundaries.x)
            {
                transform.position = new Vector2(boundaries.y, transform.position.y);
            }
        }
    }
}