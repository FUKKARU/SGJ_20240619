using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IA;

namespace Main
{
    public class Player : MonoBehaviour
    {
        [Header("�ړ����x")]
        [SerializeField] float moveSpeed;
        [Header("�J��������̃I�t�Z�b�g")]
        [SerializeField] float offsetY;
        [Header("�u���X�p��")]
        [SerializeField] float putSpan;
        [Header("�������v���t�@�u")]
        [SerializeField] Item[] itemPrefab = new Item[3];
        [Header("�Q�[�~���O��v���t�@�u")]
        [SerializeField] Item specialPrefab;

        float putTimer;
        int previousID;
        Camera cam;
        Item currentItem;

        private void Start()
        {
            // ���C���J�����擾
            cam = Camera.main;

            putTimer = 0;
        }

        private void Update()
        {
            // ���������Z�b�g
            if (currentItem == null)
            {
                SetItem();
            }

            // ��������u��
            if (CanPut())
            {
                putTimer = 0;
                PutItem();
            }
            else
            {
                putTimer += Time.deltaTime;
            }

            // �ړ�
            float inputX = (InputGetter.Instance.Main_IsRightHold ? 1 : 0) + (InputGetter.Instance.Main_IsLeftHold ? -1 : 0);
            if ((transform.position.x >= 10f && inputX > 0) || (transform.position.x <= -10f && inputX < 0))
            {
                inputX = 0;
            }
            transform.position += Vector3.right * inputX * moveSpeed * Time.deltaTime;

            // �Ǐ]
            transform.position = new Vector3(transform.position.x, cam.transform.position.y + offsetY, transform.position.z); // y�́i�J�����̈ʒu) + (�I�t�Z�b�g)
        }

        // ���������Z�b�g
        private void SetItem()
        {
            int r = Random.Range(0, 4);
            
            if (r > 0 || previousID >= 8)
            {
                r = Random.Range(0, 8);
            }
            else
            {
                r = Random.Range(8, itemPrefab.Length);
            }
            
            if (r == 9)
            {
                int rMikado = Random.Range(0, 2);
                if (rMikado == 0)
                {
                    currentItem = Instantiate(specialPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    currentItem = Instantiate(itemPrefab[r], transform.position, Quaternion.identity);
                }
            }
            else
            {
                currentItem = Instantiate(itemPrefab[r], transform.position, Quaternion.identity);
            }

            currentItem.transform.SetParent(transform);
            currentItem.id = r;
            previousID = r;

            // �l�łȂ��Ȃ�50%�Ŕ��]
            if (r < 8)
            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    currentItem.transform.localScale = new Vector3(-currentItem.transform.localScale.x, currentItem.transform.localScale.y, currentItem.transform.localScale.z);
                }
            }
        }

        // ��������u��
        private void PutItem()
        {
            currentItem.Set();
            currentItem = null;
        }

        // �u�����Ԃ��`�F�b�N
        private bool CanPut()
        {
            return ((InputGetter.Instance.Main_IsSubmit || InputGetter.Instance.Main_IsSubmitHold) // ���͂��Ă��邩
                && putTimer >= putSpan               // �O�̐ݒu���玞�Ԃ��o�߂������i�A���ł͒u���Ȃ��j
                && currentItem != null);             // �������̓Z�b�g����Ă��邩
        }
    }
}
