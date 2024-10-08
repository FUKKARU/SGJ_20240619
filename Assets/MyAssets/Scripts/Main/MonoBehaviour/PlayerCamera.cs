using System.Collections;
using System.Collections.Generic;
using SO;
using UnityEngine;

namespace Main
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("追従速度")]
        [SerializeField] float trackSpeed = 1;
        Vector3 TallestItemPos = new Vector3(0, 0, 0);
        Vector3 currentCameraPos = new Vector3(0, 0, 0);

        SO_Tags tags;
        void Awake()
        {
            tags = SO_Tags.Entity;
            currentCameraPos = transform.position;
        }

        void Update()
        {
            // 常に竹に追従
            if (currentCameraPos.y <= 28) // 28は月の座標-2
            {
                transform.position = Vector3.MoveTowards(transform.position, currentCameraPos, trackSpeed * Time.deltaTime);
            }

            // 追従座標を探索
            // CreateNewCameraPosition();
        }

        public void CreateNewCameraPosition()
        {
            GetTallestBambooPos();
            currentCameraPos.x = TallestItemPos.x;
            currentCameraPos.y = TallestItemPos.y;
        }

        void GetTallestBambooPos()
        {
            TallestItemPos.y = 0;//崩れるかもしれないのでy座標ゼロから探す
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tags.ItemTag))
            {
                Item item = obj.GetComponent<Item>();

                // 落下していない　かつ　接地していない なら
                if (!item.IsActive())
                {
                    continue;
                }

                if (TallestItemPos.y < item.transform.position.y)
                {
                    TallestItemPos.y = item.transform.position.y;
                }
            }
        }
    }
}
