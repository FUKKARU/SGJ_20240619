using System.Collections;
using System.Collections.Generic;
using SO;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Vector3 TallestItemPos = new Vector3(0, 0, 0);

    SO_Tags tags;
    void Awake()
    {
        tags = SO_Tags.Entity;
    }

    void Update()
    {
        // èÌÇ…í|Ç…í«è]
        // CreateNewCameraPosition();
    }

    public void CreateNewCameraPosition()
    {
        GetTallestBambooPos();
        Vector3 currentCameraPos = gameObject.transform.position;
        currentCameraPos.x = TallestItemPos.x;
        currentCameraPos.y = TallestItemPos.y;
        gameObject.transform.position = currentCameraPos;
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
