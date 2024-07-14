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
        // 常に竹に追従
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
