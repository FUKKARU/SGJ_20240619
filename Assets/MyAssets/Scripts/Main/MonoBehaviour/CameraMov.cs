using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour//ÉJÉÅÉâÇ…Ç¬ÇØÇƒÇÀ
{
    Vector3 TallestBambooPos = new Vector3 (0, 0, 0);

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
        currentCameraPos.x = TallestBambooPos.x;
        currentCameraPos.y = TallestBambooPos.y;
        gameObject.transform.position = currentCameraPos;
    }

    void GetTallestBambooPos()
    {
        TallestBambooPos.y = 0;//ïˆÇÍÇÈÇ©Ç‡ÇµÇÍÇ»Ç¢ÇÃÇ≈yç¿ïWÉ[ÉçÇ©ÇÁíTÇ∑
        foreach (GameObject bamboo in GameObject.FindGameObjectsWithTag(tags.ItemTag))
        {
            if(TallestBambooPos.y < bamboo.transform.position.y && bamboo.GetComponent<Bamboo_FallController>().collisionWithMtFuji)
            {
                TallestBambooPos.y = bamboo.transform.position.y;
            }
        }
    }
}
