using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IA;
using SO;
using Title.SO;

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
        [Header("�X�^�[�g�{�^��(�ʏ�/�z�o�[/�N���b�N)")] public List<Sprite> StartButtonSprites = new();
        [Header("�Q�[���I���{�^��(�ʏ�/�z�o�[/�N���b�N)")] public List<Sprite> QuitButtonSprites = new();
        [Space(25)]
        [Header("�V�[�����Q�[���I�u�W�F�N�g�̎Q��")]
        [Header("�X�^�[�g�{�^��")] public SpriteRenderer StartButton;
        [Header("�Q�[���I���{�^��")] public SpriteRenderer QuitButton;

        // 0�Ȃ�{�^���̑I���Ȃ��A1�Ȃ�X�^�[�g�{�^���A2�Ȃ�Q�[���I���{�^��
        private int _buttonSelecting = 0;
        private int buttonSelecting
        {
            get { return _buttonSelecting; }
            set { _buttonSelecting = Mathf.Clamp(value, 0, 2); }
        }

        // true�Ȃ�A��؂̃{�^�����͂��󂯕t���Ȃ�
        private bool _buttonClicked = false;

        // UniTask�̃L�����Z�����[�V�����g�[�N��(���̃Q�[���I�u�W�F�N�g���j�����ꂽ�ۂɒ�~����)
        private CancellationToken _ct;

        private void Start()
        {
            // �L�����Z�����[�V�����g�[�N���̐ݒ�
            _ct = this.GetCancellationTokenOnDestroy();

            // �{�^���ɏ����X�v���C�g���Z�b�g
            StartButton.sprite = StartButtonSprites[ButtonState.Normal];
            QuitButton.sprite = QuitButtonSprites[ButtonState.Normal];
        }

        private void Update()
        {
            // �{�^�����N���b�N����Ă��Ȃ��Ȃ�...
            if (!_buttonClicked)
            {
                #region �y1�z�܂��ŏ��Ƀ{�^���̑I����Ԃ��X�V��... (�v���C���[�̓��͂��󂯕t����)

                // case : �{�^�����I������Ă��Ȃ��ꍇ
                if (_buttonSelecting == ButtonIndex.None)
                {
                    // �㉺�L�[�̂ǂ���������Ă��A�X�^�[�g�{�^����I��������Ԃɂ���B
                    if (InputGetter.Instance.Main_IsDown || InputGetter.Instance.Main_IsUp)
                    {
                        _buttonSelecting = ButtonIndex.Start;
                    }
                }
                // case : �X�^�[�g�{�^����������Ă���ꍇ
                else if (_buttonSelecting == ButtonIndex.Start)
                {
                    // �㉺�L�[�̂ǂ���������Ă��A�Q�[���I���{�^����I��������Ԃɂ���B
                    if (InputGetter.Instance.Main_IsDown || InputGetter.Instance.Main_IsUp)
                    {
                        _buttonSelecting = ButtonIndex.Quit;
                    }
                }
                // case : �Q�[���I���{�^����������Ă���ꍇ
                else if (_buttonSelecting == ButtonIndex.Quit)
                {
                    // �㉺�L�[�̂ǂ���������Ă��A�X�^�[�g�{�^����I��������Ԃɂ���B
                    if (InputGetter.Instance.Main_IsDown || InputGetter.Instance.Main_IsUp)
                    {
                        _buttonSelecting = ButtonIndex.Start;
                    }
                }

                #endregion

                #region �y2�z���Ƀ{�^���̃X�v���C�g���X�V��...

                // case : �{�^�����I������Ă��Ȃ��ꍇ
                if (buttonSelecting == ButtonIndex.None)
                {
                    StartButton.sprite = StartButtonSprites[ButtonState.Normal];
                    QuitButton.sprite = QuitButtonSprites[ButtonState.Normal];
                }
                // case : �X�^�[�g�{�^�����I������Ă���ꍇ
                else if (buttonSelecting == ButtonIndex.Start)
                {
                    StartButton.sprite = StartButtonSprites[ButtonState.Hover];
                    QuitButton.sprite = QuitButtonSprites[ButtonState.Normal];
                }
                // case : �Q�[���I���{�^�����I������Ă���ꍇ
                else if (buttonSelecting == ButtonIndex.Quit)
                {
                    StartButton.sprite = StartButtonSprites[ButtonState.Normal];
                    QuitButton.sprite = QuitButtonSprites[ButtonState.Hover];
                }
                else
                {
                    throw new System.Exception("�����ȃC���f�b�N�X�ԍ��ł�");
                }

                #endregion

                #region �y3�z�Ō�Ƀv���C���[�̃{�^���N���b�N���󂯕t���A�K�؂ȏ������s���B

                // case : �{�^�����I������Ă��Ȃ��ꍇ
                if (buttonSelecting == ButtonIndex.None)
                {
                    // �������Ȃ��I
                }
                // case : �X�^�[�g�{�^�����I������Ă���ꍇ
                else if (buttonSelecting == ButtonIndex.Start)
                {
                    // �X�^�[�g�{�^�����I�����ꂽ��ԂŁA���肪�����ꂽ�Ȃ�...
                    if (InputGetter.Instance.Main_IsSubmit)
                    {
                        // ���̃t���[���̌�A��؂̓��͂��󂯕t���Ȃ�
                        _buttonClicked = true;

                        // �X�v���C�g���X�V(����ȍ~�A�{�^���̃X�v���C�g�̍X�V�͈�؍s���Ȃ��z��)
                        StartButton.sprite = StartButtonSprites[ButtonState.Click];

                        // ���C���V�[���ɑJ��
                        SceneChange(SO_SceneName.Entity.Main, false, _ct).Forget();
                    }
                }
                // case : �Q�[���I���{�^�����I������Ă���ꍇ
                else if (buttonSelecting == ButtonIndex.Quit)
                {
                    // �Q�[���I���{�^�����I�����ꂽ��ԂŁA���肪�����ꂽ�Ȃ�...
                    if (InputGetter.Instance.Main_IsSubmit)
                    {
                        // ���̃t���[���̌�A��؂̓��͂��󂯕t���Ȃ�
                        _buttonClicked = true;

                        // �X�v���C�g���X�V(����ȍ~�A�{�^���̃X�v���C�g�̍X�V�͈�؍s���Ȃ��z��)
                        QuitButton.sprite = QuitButtonSprites[ButtonState.Click];

                        // �Q�[���I���I
                        QuitGame(_ct).Forget();
                    }
                }
                else
                {
                    throw new Exception("�����ȃC���f�b�N�X�ԍ��ł�");
                }

                #endregion
            }
            // �{�^�����N���b�N����Ă���Ȃ�...
            else
            {
                // �������Ȃ��I
            }
        }

        // �V�[���J��
        async UniTask SceneChange(string toSceneName, bool isAsync, CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(SO_Title.Entity.OnButtonClickWaitDur));

            // �񓯊��J��
            if (isAsync)
            {
                // �܂�������
            }
            // �����J��
            else
            {
                SceneManager.LoadScene(toSceneName);
            }
        }

        // �Q�[���I��
        async UniTask QuitGame(CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(SO_Title.Entity.OnButtonClickWaitDur));

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    // �{�^���̏��
    public static class ButtonState
    {
        public static int Normal = 0;
        public static int Hover = 1;
        public static int Click = 2;
    }

    // �{�^���̑I�����
    public static class ButtonIndex
    {
        public static int None = 0;
        public static int Start = 1;
        public static int Quit = 2;
    }
}