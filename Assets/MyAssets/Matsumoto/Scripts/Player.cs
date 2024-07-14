using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�ړ����x")]
    [SerializeField] float moveSpeed;
    [Header("�J��������̃I�t�Z�b�g")]
    [SerializeField] float offsetY;
    [Header("�u���X�p��")]
    [SerializeField] float putSpan;
    [Header("�|�v���t�@�u")]
    [SerializeField] Bamboo bambooPrefab;
    
    float putTimer;
    Camera cam;

    private void Start()
    {
        // ���C���J�����擾
        cam = Camera.main;

        putTimer = 0;
    }

    private void Update()
    {
        // �|��u��
        if (Input.GetKeyDown(KeyCode.Mouse0) && putTimer >= putSpan) // ���Ԃ������Ēu���i�A���ł͒u���Ȃ��j
        {
            putTimer = 0;
            PutBamboo();
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

    // �|��u��
    private void PutBamboo()
    {
        Instantiate(bambooPrefab, transform.position, Quaternion.identity);
    }
}
