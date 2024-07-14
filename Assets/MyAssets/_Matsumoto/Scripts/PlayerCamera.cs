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
        // ��ɒ|�ɒǏ]
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
