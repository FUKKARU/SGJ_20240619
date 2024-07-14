using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] float moveSpeed;
    [Header("カメラからのオフセット")]
    [SerializeField] float offsetY;
    [Header("置くスパン")]
    [SerializeField] float putSpan;
    [Header("竹プレファブ")]
    [SerializeField] Bamboo bambooPrefab;
    
    float putTimer;
    Camera cam;

    private void Start()
    {
        // メインカメラ取得
        cam = Camera.main;

        putTimer = 0;
    }

    private void Update()
    {
        // 竹を置く
        if (Input.GetKeyDown(KeyCode.Mouse0) && putTimer >= putSpan) // 時間をおいて置く（連続では置けない）
        {
            putTimer = 0;
            PutBamboo();
        }
        else
        {
            putTimer += Time.deltaTime;
        }

        // 移動
        float inputX = Input.GetAxisRaw("Horizontal");
        transform.position += Vector3.right * inputX * moveSpeed * Time.deltaTime;

        // 追従
        transform.position = new Vector3(transform.position.x, cam.transform.position.y + offsetY, transform.position.z); // yは（カメラの位置) + (オフセット)
    }

    // 竹を置く
    private void PutBamboo()
    {
        Instantiate(bambooPrefab, transform.position, Quaternion.identity);
    }
}
