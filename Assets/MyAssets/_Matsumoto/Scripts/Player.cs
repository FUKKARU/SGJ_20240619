using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �����_���ŗ��������o�Ă���
    // ���Ƃ��O�ɕ\��������
    // 5�b�o�����珟��ɗ�����
    [Header("�ړ����x")]
    [SerializeField] float moveSpeed;
    [Header("�J��������̃I�t�Z�b�g")]
    [SerializeField] float offsetY;
    [Header("�u���X�p��")]
    [SerializeField] float putSpan;
    [Header("�������v���t�@�u")]
    [SerializeField] Item[] itemPrefab = new Item[3];
    
    float putTimer;
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
        float inputX = Input.GetAxisRaw("Horizontal");
        transform.position += Vector3.right * inputX * moveSpeed * Time.deltaTime;

        // �Ǐ]
        transform.position = new Vector3(transform.position.x, cam.transform.position.y + offsetY, transform.position.z); // y�́i�J�����̈ʒu) + (�I�t�Z�b�g)
    }

    // ���������Z�b�g
    private void SetItem()
    {
        int r = Random.Range(0, 3);
        currentItem = Instantiate(itemPrefab[r], transform.position, Quaternion.identity);
        currentItem.transform.SetParent(transform);
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
        return (Input.GetKeyDown(KeyCode.Mouse0) // ���͂��Ă��邩
            && putTimer >= putSpan               // �O�̐ݒu���玞�Ԃ��o�߂������i�A���ł͒u���Ȃ��j
            && currentItem != null);             // �������̓Z�b�g����Ă��邩
    }
}
