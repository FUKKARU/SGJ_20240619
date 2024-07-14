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

        [Header("�X�v���C�g")]
        [Header("�X�^�[�g�{�^��(�ʏ�/�z�o�[/�N���b�N)")] public List<Sprite> StartButton = new();
        [Header("�Q�[���I���{�^��(�ʏ�/�z�o�[/�N���b�N)")] public List<Sprite> QuitButton = new();
    }
}