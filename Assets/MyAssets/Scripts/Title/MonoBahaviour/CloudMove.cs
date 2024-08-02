using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace main
{
    public class CloudMove : MonoBehaviour
    {
        [SerializeField] private float cloudMoveSpeed = 1.0f; //雲の移動速度
        [Header("境界線設定;x=左,y=右の境界線")]
        [SerializeField] private Vector2 boundaries = new Vector2(-13.0f,13.0f); //画面x=左,y=右の境界線
        [SerializeField] private int direction = 1; //移動方向(1は右、-1左)

        private void Start()
        {
            
        }

        private void Update()
        {
            // x軸に沿ってオブジェクトを移動させる
            transform.Translate(Vector2.right * cloudMoveSpeed * direction * Time.deltaTime);

            // オブジェクトが右側の境界を超えたら、左側の境界に移動させる
            if (transform.position.x > boundaries.y)
            {
                transform.position = new Vector2(boundaries.x, transform.position.y);
            }
            // オブジェクトが左側の境界を超えたら、右側の境界に移動させる
            else if (transform.position.x < boundaries.x)
            {
                transform.position = new Vector2(boundaries.y, transform.position.y);
            }
        }
    }
}