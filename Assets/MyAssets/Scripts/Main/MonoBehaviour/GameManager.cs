using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.SO;
using Cysharp.Threading.Tasks;
using IA;
using General;
using SO;
using System.Threading;

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

        // UniTask�̃L�����Z�����[�V�����g�[�N��(���̃Q�[���I�u�W�F�N�g���j�����ꂽ�ۂɒ�~����)
        private CancellationToken _ct;

        private void Start()
        {
            // �L�����Z�����[�V�����g�[�N���̐ݒ�
            _ct = this.GetCancellationTokenOnDestroy();
        }

        // ���҂��u���I�������ɁA�K���Ă΂��ׂ����\�b�h(���݂̍��x�������ɓ���邱��)
        public async void AfterPlace(int heightParam)
        {
            // �y1�z�܂����x���X�V��...
            height = heightParam;

            // �y2�z���ɃN���A������s��...(�N���A�ɂȂ��Ă���Ȃ炱���ŏI��)
            if (height >= SO_Main.Entity.MaxHeight)
            {
                _gameResult = Result.Clear;

                // �N���A�̏����𔭉΂��A�����ŏ����I��
                await Clear(turn);
                return;
            }

            // �y3�z���Ƀ^�[������1���₵...
            turn++;

            // �y4�z���ɃQ�[���I�[�o�[�̏������s��...(�Q�[���I�[�o�[�ɂȂ��Ă���Ȃ炱���ŏI��)
            if (turn >= SO_Main.Entity.MaxTurn)
            {
                _gameResult = Result.Over;

                // �Q�[���I�[�o�[�̏����𔭉΂��A�����ŏ����I��
                await Over(height);
                return;
            }

            // �y5�z�Ō�ɑ���̃^�[���ɂ���
            _isPlayerTurn = !_isPlayerTurn;
        }

        // �N���A�̏���(�N���A���̃^�[�����������ɓ���邱��)
        async UniTask Clear(int turnParam)
        {
            // UI�֘A�̏���(������)

            // ��A�̏������I�������A����{�^���������ꂽ��^�C�g���ɖ߂�
            while (true)
            {
                if (InputGetter.Instance.Main_IsSubmit)
                {
                    await Flow.SceneChange(SO_SceneName.Entity.Title, false, SO_Main.Entity.OnButtonClickWaitDur, _ct);
                }

                await UniTask.Yield();
            }
        }

        // �Q�[���I�[�o�[�̏���(�Q�[���I�[�o�[���̍��x�������ɓ���邱��)
        async UniTask Over(int heightParam)
        {
            // UI�֘A�̏���(������)

            // ��A�̏������I�������A����{�^���������ꂽ��^�C�g���ɖ߂�
            while (true)
            {
                if (InputGetter.Instance.Main_IsSubmit)
                {
                    await Flow.SceneChange(SO_SceneName.Entity.Title, false, SO_Main.Entity.OnButtonClickWaitDur, _ct);
                }

                await UniTask.Yield();
            }
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