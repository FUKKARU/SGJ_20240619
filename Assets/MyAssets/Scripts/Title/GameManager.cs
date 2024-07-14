using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Title
{
    public class GameManager : MonoBehaviour
    {
        #region
        public static GameManager Instance { get; set; } = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        [Header("スプライト")]
        [Header("スタートボタン(通常/ホバー/クリック)")] public List<Sprite> StartButton = new();
        [Header("ゲーム終了ボタン(通常/ホバー/クリック)")] public List<Sprite> QuitButton = new();
    }
}