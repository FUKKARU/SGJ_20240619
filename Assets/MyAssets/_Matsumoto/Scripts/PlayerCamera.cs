using System.Collections;
using System.Collections.Generic;
using SO;
using UnityEngine;

namespace Main
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("í«è]ë¨ìx")]
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
            // èÌÇ…í|Ç…í«è]
            transform.position = Vector3.MoveTowards(transform.position, currentCameraPos, trackSpeed * Time.deltaTime);

            // í«è]ç¿ïWÇíTçı
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
            TallestItemPos.y = 0;//ïˆÇÍÇÈÇ©Ç‡ÇµÇÍÇ»Ç¢ÇÃÇ≈yç¿ïWÉ[ÉçÇ©ÇÁíTÇ∑
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tags.ItemTag))
            {
                Item item = obj.GetComponent<Item>();

                // óéâ∫ÇµÇƒÇ¢Ç»Ç¢Å@Ç©Ç¬Å@ê⁄ínÇµÇƒÇ¢Ç»Ç¢ Ç»ÇÁ
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
