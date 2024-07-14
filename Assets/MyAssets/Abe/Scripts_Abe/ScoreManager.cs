using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameObject reachedmaxHeightText, gameOverText, gameClearText;
    SO_Tags tags;
    float maxPos = 1;
    void Awake()
    {
        tags = SO_Tags.Entity;
    }

    void Start()
    {
        reachedmaxHeightText.SetActive(false); 
        gameOverText.SetActive(false);
        gameClearText.SetActive(false);
    }

    void Update()
    {
        
    }

    [ContextMenu("FunctionExecution")]
    void scoreShow()
    {
        reachedmaxHeightText.SetActive(true);
        Vector3 TallestBambooPos = new Vector3 (0, 0, 0);
        TallestBambooPos.y = 0;//ïˆÇÍÇÈÇ©Ç‡ÇµÇÍÇ»Ç¢ÇÃÇ≈yç¿ïWÉ[ÉçÇ©ÇÁíTÇ∑
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(tags.ItemTag))
        {
            if (TallestBambooPos.y < item.transform.position.y && item.GetComponent<Item>().isGround)
            {
                TallestBambooPos.y = item.transform.position.y;
            }
        }
        if (TallestBambooPos.y >= maxPos)
        {
            gameClearText.SetActive(true);
        }
        else
        {
            gameOverText.SetActive(true);
        }
    }

}
