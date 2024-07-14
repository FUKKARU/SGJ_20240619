using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.SO;

namespace Main
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

        // ���݂̃^�[����(0~)(�ő�^�[�����ɒB������Q�[���I�[�o�[)
        private int _turn = 0;
        private int turn
        {
            get { return _turn; }
            set { _turn = Mathf.Clamp(value, 0, SO_Main.Entity.MaxTurn); }
        }

        // ���݂̍��x(�ő�l�ɒB������N���A)
        private int _height = 0;
        private int height
        {
            get { return _height; }
            set { _height = Mathf.Clamp(value, 0, SO_Main.Entity.MaxHeight); }
        }

        // �v���C���[�̔Ԃł��邩�ǂ���
        private bool _isPlayerTurn = true;
        public bool IsPlayerTurn => _isPlayerTurn;

        // �Q�[���̌���(0�Ȃ�܂��Q�[�����I����Ă��Ȃ��A1�Ȃ�N���A�ɂȂ��Ă���A2�Ȃ�Q�[���I�[�o�[�ɂȂ��Ă���)
        private int _gameResult = Result.None;
        public int GameResult => _gameResult;

        // ���҂��u���I�������ɁA�K���Ă΂��ׂ����\�b�h(���݂̍��x�������ɓ���邱��)
        public void AfterPlace(int heightParam)
        {
            // �y1�z�܂����x���X�V��...
            height = heightParam;

            // �y2�z���ɃN���A������s��...(�N���A�ɂȂ��Ă���Ȃ炱���ŏI��)
            if (height >= SO_Main.Entity.MaxHeight)
            {
                _gameResult = Result.Clear;

                // �N���A�̏���

                return;
            }

            // �y3�z���Ƀ^�[������1���₵...
            turn++;

            // �y4�z���ɃQ�[���I�[�o�[�̏������s��...(�Q�[���I�[�o�[�ɂȂ��Ă���Ȃ炱���ŏI��)
            if (turn >= SO_Main.Entity.MaxTurn)
            {
                _gameResult = Result.Over;

                // �Q�[���I�[�o�[�̏���

                return;
            }

            // �y5�z�Ō�ɑ���̃^�[���ɂ���
            _isPlayerTurn = !_isPlayerTurn;
        }
    }

    // �Q�[���̌���(0�Ȃ�܂��Q�[�����I����Ă��Ȃ��A1�Ȃ�N���A�ɂȂ��Ă���A2�Ȃ�Q�[���I�[�o�[�ɂȂ��Ă���)
    public static class Result
    {
        public static int None = 0;
        public static int Clear = 1;
        public static int Over = 2;
    }
}