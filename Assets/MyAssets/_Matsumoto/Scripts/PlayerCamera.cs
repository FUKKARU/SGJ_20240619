using System.Collections;
using System.Collections.Generic;
using SO;
using UnityEngine;

namespace Main
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("�Ǐ]���x")]
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
            // ��ɒ|�ɒǏ]
            transform.position = Vector3.MoveTowards(transform.position, currentCameraPos, trackSpeed * Time.deltaTime);

            // �Ǐ]���W��T��
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
            TallestItemPos.y = 0;//����邩������Ȃ��̂�y���W�[������T��
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tags.ItemTag))
            {
                Item item = obj.GetComponent<Item>();

                // �������Ă��Ȃ��@���@�ڒn���Ă��Ȃ� �Ȃ�
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
