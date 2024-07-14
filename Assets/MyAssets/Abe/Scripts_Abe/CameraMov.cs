using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour//カメラにつけてね
{
    Vector3 TallestBambooPos = new Vector3 (0, 0, 0);

    SO_Tags tags;
    void Awake()
    {
        tags = SO_Tags.Entity;
    }

    void Update()
    {
        CreateNewCameraPosition();
    }

    void CreateNewCameraPosition()
    {
        GetTallestBambooPos();
        Vector3 currentCameraPos = gameObject.transform.position;
        currentCameraPos.x = TallestBambooPos.x;
        currentCameraPos.y = TallestBambooPos.y;
        gameObject.transform.position = currentCameraPos;
    }

    void GetTallestBambooPos()
    {
        TallestBambooPos.y = 0;//崩れるかもしれないのでy座標ゼロから探す
        foreach (GameObject bamboo in GameObject.FindGameObjectsWithTag(tags.BambooTag))
        {
            if(TallestBambooPos.y < bamboo.transform.position.y)
            {
                TallestBambooPos.y = bamboo.transform.position.y;
            }
        }
    }
}
