using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IA;
using UnityEngine.SceneManagement;
using SO;

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
        [Header("スタートボタン(通常/ホバー/クリック)")] public List<Sprite> StartButtonSprites = new();
        [Header("ゲーム終了ボタン(通常/ホバー/クリック)")] public List<Sprite> QuitButtonSprites = new();
        [Space(25)]
        [Header("シーン内ゲームオブジェクトの参照")]
        [Header("スタートボタン")] public SpriteRenderer StartButton;
        [Header("ゲーム終了ボタン")] public SpriteRenderer QuitButton;

        // 0ならボタンの選択なし、1ならスタートボタン、2ならゲーム終了ボタン
        private int _buttonSelecting = 0;
        private int buttonSelecting
        {
            get { return _buttonSelecting; }
            set { _buttonSelecting = Mathf.Clamp(value, 0, 2); }
        }

        // trueなら、一切のボタン入力を受け付けない
        private bool _buttonClicked = false;

        private void Start()
        {
            // ボタンに初期スプライトをセット
            StartButton.sprite = StartButtonSprites[ButtonState.Normal];
            QuitButton.sprite = QuitButtonSprites[ButtonState.Normal];
        }

        private void Update()
        {
            // ボタンがクリックされていないなら...
            if (!_buttonClicked)
            {
                #region 【1】まず最初にボタンの選択状態を更新し... (プレイヤーの入力を受け付ける)

                // case : ボタンが選択されていない場合
                if (_buttonSelecting == ButtonIndex.None)
                {
                    // 上下キーのどちらを押しても、スタートボタンを選択した状態にする。
                    if (InputGetter.Instance.Main_IsDown || InputGetter.Instance.Main_IsUp)
                    {
                        _buttonSelecting = ButtonIndex.Start;
                    }
                }
                // case : スタートボタンが押されている場合
                else if (_buttonSelecting == ButtonIndex.Start)
                {
                    // 上下キーのどちらを押しても、ゲーム終了ボタンを選択した状態にする。
                    if (InputGetter.Instance.Main_IsDown || InputGetter.Instance.Main_IsUp)
                    {
                        _buttonSelecting = ButtonIndex.Quit;
                    }
                }
                // case : ゲーム終了ボタンが押されている場合
                else if (_buttonSelecting == ButtonIndex.Quit)
                {
                    // 上下キーのどちらを押しても、スタートボタンを選択した状態にする。
                    if (InputGetter.Instance.Main_IsDown || InputGetter.Instance.Main_IsUp)
                    {
                        _buttonSelecting = ButtonIndex.Start;
                    }
                }

                #endregion

                #region 【2】次にボタンのスプライトを更新し...

                // case : ボタンが選択されていない場合
                if (buttonSelecting == ButtonIndex.None)
                {
                    StartButton.sprite = StartButtonSprites[ButtonState.Normal];
                    QuitButton.sprite = QuitButtonSprites[ButtonState.Normal];
                }
                // case : スタートボタンが選択されている場合
                else if (buttonSelecting == ButtonIndex.Start)
                {
                    StartButton.sprite = StartButtonSprites[ButtonState.Hover];
                    QuitButton.sprite = QuitButtonSprites[ButtonState.Normal];
                }
                // case : ゲーム終了ボタンが選択されている場合
                else if (buttonSelecting == ButtonIndex.Quit)
                {
                    StartButton.sprite = StartButtonSprites[ButtonState.Normal];
                    QuitButton.sprite = QuitButtonSprites[ButtonState.Hover];
                }
                else
                {
                    throw new System.Exception("無効なインデックス番号です");
                }

                #endregion

                #region 【3】最後にプレイヤーのボタン操作を受け付け、適切な処理を行う。

                // case : ボタンが選択されていない場合
                if (buttonSelecting == ButtonIndex.None)
                {
                    // 何もしない！
                }
                // case : スタートボタンが選択されている場合
                else if (buttonSelecting == ButtonIndex.Start)
                {
                    // スタートボタンが選択された状態で、決定が押されたなら...
                    if (InputGetter.Instance.Main_IsSubmit)
                    {
                        // このフレームの後、一切の入力を受け付けない
                        _buttonClicked = true;

                        // メインシーンに遷移
                        SceneChange(SO_SceneName.Entity.Main);
                    }
                }
                // case : ゲーム終了ボタンが選択されている場合
                else if (buttonSelecting == ButtonIndex.Quit)
                {
                    // ゲーム終了ボタンが選択された状態で、決定が押されたなら...
                    if (InputGetter.Instance.Main_IsSubmit)
                    {
                        // このフレームの後、一切の入力を受け付けない
                        _buttonClicked = true;

                        // げーむ終了！
                        QuitGame();
                    }
                }
                else
                {
                    throw new System.Exception("無効なインデックス番号です");
                }

                #endregion
            }
            // ボタンがクリックされているなら...
            else
            {
                // 何もしない！
            }
        }

        // シーン遷移
        void SceneChange(string toSceneName, bool isAsync = false)
        {
            // 非同期遷移
            if (isAsync)
            {
                // まだ未実装
            }
            // 即時遷移
            else
            {
                SceneManager.LoadScene(toSceneName);
            }
        }

        // ゲーム終了
        void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    // ボタンの状態
    public static class ButtonState
    {
        public static int Normal = 0;
        public static int Hover = 1;
        public static int Click = 2;
    }

    // ボタンの選択状態
    public static class ButtonIndex
    {
        public static int None = 0;
        public static int Start = 1;
        public static int Quit = 2;
    }
}