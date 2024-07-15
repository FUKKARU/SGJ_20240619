using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] GameObject reachedmaxHeightText, gameOverText, gameClearText;
        [SerializeField] GameObject stagePos;
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

        void scoreShow()
        {
            reachedmaxHeightText.SetActive(true);
            Vector3 TallestBambooPos = new Vector3(0, 0, 0);
            TallestBambooPos.y = 0;//����邩������Ȃ��̂�y���W�[������T��
            foreach (GameObject item in GameObject.FindGameObjectsWithTag(tags.ItemTag))
            {
                if (TallestBambooPos.y < item.transform.position.y && item.GetComponent<Item>().isGround)
                {
                    TallestBambooPos.y = item.transform.position.y;
                }
            }
            if (TallestBambooPos.y + stagePos.transform.position.y >= maxPos)//�S�̓X�e�[�W�̌덷
            {
                gameClearText.SetActive(true);
            }
            else
            {
                gameOverText.SetActive(true);
            }
        }

    }
}


