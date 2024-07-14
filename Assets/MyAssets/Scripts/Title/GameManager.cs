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
        //[Header("スタートボタン")] public Sprite[2] StartButton = new();
    }
}