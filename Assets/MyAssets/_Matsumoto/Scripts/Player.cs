using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IA;

namespace Main
{
    public class Player : MonoBehaviour
    {
        [Header("移動速度")]
        [SerializeField] float moveSpeed;
        [Header("カメラからのオフセット")]
        [SerializeField] float offsetY;
        [Header("置くスパン")]
        [SerializeField] float putSpan;
        [Header("落ち物プレファブ")]
        [SerializeField] Item[] itemPrefab = new Item[3];
        [Header("ゲーミング帝プレファブ")]
        [SerializeField] Item specialPrefab;

        float putTimer;
        int previousID;
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
            float inputX = (InputGetter.Instance.Main_IsRightHold ? 1 : 0) + (InputGetter.Instance.Main_IsLeftHold ? -1 : 0);
            if ((transform.position.x >= 10f && inputX > 0) || (transform.position.x <= -10f && inputX < 0))
            {
                inputX = 0;
            }
            transform.position += Vector3.right * inputX * moveSpeed * Time.deltaTime;

            // 追従
            transform.position = new Vector3(transform.position.x, cam.transform.position.y + offsetY, transform.position.z); // yは（カメラの位置) + (オフセット)
        }

        // 落ち物をセット
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

            // 人でないなら50%で反転
            if (r < 8)
            {
                r = Random.Range(0, 2);
                if (r == 0)
                {
                    currentItem.transform.localScale = new Vector3(-currentItem.transform.localScale.x, currentItem.transform.localScale.y, currentItem.transform.localScale.z);
                }
            }
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
            return ((InputGetter.Instance.Main_IsSubmit || InputGetter.Instance.Main_IsSubmitHold) // 入力しているか
                && putTimer >= putSpan               // 前の設置から時間が経過したか（連続では置けない）
                && currentItem != null);             // 落ち物はセットされているか
        }
    }
}
