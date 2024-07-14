using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ランダムで落ち物が出てくる
    // 落とす前に表示させる
    // 5秒経ったら勝手に落ちる
    [Header("移動速度")]
    [SerializeField] float moveSpeed;
    [Header("カメラからのオフセット")]
    [SerializeField] float offsetY;
    [Header("置くスパン")]
    [SerializeField] float putSpan;
    [Header("落ち物プレファブ")]
    [SerializeField] Item[] itemPrefab = new Item[3];
    
    float putTimer;
    Camera cam;
    Item currentItem;

    private void Start()
    {
        // メインカメラ取得
        cam = Camera.main;

        putTimer = 0;
    }

    private void Update()
    {
        // 落ち物をセット
        if (currentItem == null)
        {
            SetItem();
        }

        // 落ち物を置く
        if (CanPut())
        {
            putTimer = 0;
            PutItem();
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

    // 落ち物をセット
    private void SetItem()
    {
        int r = Random.Range(0, 3);
        currentItem = Instantiate(itemPrefab[r], transform.position, Quaternion.identity);
        currentItem.transform.SetParent(transform);
    }

    // 落ち物を置く
    private void PutItem()
    {
        currentItem.Set();
        currentItem = null;
    }

    // 置ける状態かチェック
    private bool CanPut()
    {
        return (Input.GetKeyDown(KeyCode.Mouse0) // 入力しているか
            && putTimer >= putSpan               // 前の設置から時間が経過したか（連続では置けない）
            && currentItem != null);             // 落ち物はセットされているか
    }
}
