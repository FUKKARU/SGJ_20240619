using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour//�J�����ɂ��Ă�
{
    Vector3 TallestBambooPos = new Vector3 (0, 0, 0);

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
        currentCameraPos.x = TallestBambooPos.x;
        currentCameraPos.y = TallestBambooPos.y;
        gameObject.transform.position = currentCameraPos;
    }

    void GetTallestBambooPos()
    {
        TallestBambooPos.y = 0;//����邩������Ȃ��̂�y���W�[������T��
        foreach (GameObject bamboo in GameObject.FindGameObjectsWithTag(tags.ItemTag))
        {
            if(TallestBambooPos.y < bamboo.transform.position.y && bamboo.GetComponent<Bamboo_FallController>().collisionWithMtFuji)
            {
                TallestBambooPos.y = bamboo.transform.position.y;
            }
        }
    }
}
