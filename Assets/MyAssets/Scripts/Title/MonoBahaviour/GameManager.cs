using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IA;
using SO;
using General;

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
        [Header("スタートボタン(通常/選択)")] public List<Sprite> StartButtonSprites = new();
        [Header("ゲーム終了ボタン(通常/選択)")] public List<Sprite> QuitButtonSprites = new();
        [Header("クレジット表示ボタン(通常/選択)")] public List<Sprite> CreditButtonSprites = new();
        [Space(25)]
        [Header("シーン内ゲームオブジェクトの参照")]
        [Header("スタートボタン")] public SpriteRenderer StartButton;
        [Header("ゲーム終了ボタン")] public SpriteRenderer QuitButton;
        [Header("クレジット表示ボタン")] public SpriteRenderer CreditButton;
        [Space(25)]
        [Header("クレジットUI")] public GameObject CreditUI;

        // 0ならボタンの選択なし、1ならスタートボタン、2ならゲーム終了ボタン、3ならクレジット表示ボタン
        private int _buttonSelecting = 0;
        private int buttonSelecting
        {
            get { return _buttonSelecting; }
            set { _buttonSelecting = Mathf.Clamp(value, 0, 3); }
        }

        // trueなら、一切のボタン入力を受け付けない
        private bool _buttonClicked = false;

        // UniTaskのキャンセラレーショントークン(このゲームオブジェクトが破棄された際に停止する)
        private CancellationToken _ct;

        private void Start()
        {
            // キャンセラレーショントークンの設定
            _ct = this.GetCancellationTokenOnDestroy();

            // クレジットUIを非表示にする
            CreditUI.SetActive(false);

            // ボタンに初期スプライトをセット
            StartButton.sprite = StartButtonSprites[ButtonState.Normal];
            QuitButton.sprite = QuitButtonSprites[ButtonState.Normal];
            CreditButton.sprite = CreditButtonSprites[ButtonState.Normal];
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
                    // 左右キーのどちらを押しても、スタートボタンを選択した状態にする
                    if (InputGetter.Instance.Main_IsLeft || InputGetter.Instance.Main_IsRight)
                    {
                        _buttonSelecting = ButtonIndex.Start;
                    }
                }
                // case : スタートボタンが押されている場合
                else if (_buttonSelecting == ButtonIndex.Start)
                {
                    // 右キーを押すと、ゲーム終了ボタンを選択した状態にする
                    if (InputGetter.Instance.Main_IsRight)
                    {
                        _buttonSelecting = ButtonIndex.Quit;
                    }
                }
                // case : ゲーム終了ボタンが押されている場合
                else if (_buttonSelecting == ButtonIndex.Quit)
                {
                    // 左キーを押すと、スタートボタンを選択した状態にする
                    if (InputGetter.Instance.Main_IsLeft)
                    {
                        _buttonSelecting = ButtonIndex.Start;
                    }
                    // 右キーを押すと、クレジット表示ボタンを選択した状態にする
                    else if (InputGetter.Instance.Main_IsRight)
                    {
                        _buttonSelecting = ButtonIndex.Credit;
                    }
                }
                // case : クレジット表示ボタンが押されている場合
                else if (_buttonSelecting == ButtonIndex.Credit)
                {
                    // 左キーを押すと、ゲーム終了ボタンを選択した状態にする
                    if (InputGetter.Instance.Main_IsLeft)
                    {
                        _buttonSelecting = ButtonIndex.Quit;
                    }
                }

                #endregion

                #region 【2】次にボタンのスプライトを更新し...

                // case : ボタンが選択されていない場合
                if (buttonSelecting == ButtonIndex.None)
                {
                    StartButton.sprite = StartButtonSprites[ButtonState.Normal];
                    QuitButton.sprite = QuitButtonSprites[ButtonState.Normal];
                    CreditButton.sprite = CreditButtonSprites[ButtonState.Normal];
                }
                // case : スタートボタンが選択されている場合
                else if (buttonSelecting == ButtonIndex.Start)
                {
                    StartButton.sprite = StartButtonSprites[ButtonState.Select];
                    QuitButton.sprite = QuitButtonSprites[ButtonState.Normal];
                    CreditButton.sprite = CreditButtonSprites[ButtonState.Normal];
                }
                // case : ゲーム終了ボタンが選択されている場合
                else if (buttonSelecting == ButtonIndex.Quit)
                {
                    StartButton.sprite = StartButtonSprites[ButtonState.Normal];
                    QuitButton.sprite = QuitButtonSprites[ButtonState.Select];
                    CreditButton.sprite = CreditButtonSprites[ButtonState.Normal];
                }
                // case : クレジット表示ボタンが選択されている場合
                else if (buttonSelecting == ButtonIndex.Credit)
                {
                    StartButton.sprite = StartButtonSprites[ButtonState.Normal];
                    QuitButton.sprite = QuitButtonSprites[ButtonState.Normal];
                    CreditButton.sprite = CreditButtonSprites[ButtonState.Select];
                }
                else
                {
                    throw new Exception("無効なインデックス番号です");
                }

                #endregion

                #region 【3】最後にプレイヤーのボタンクリックを受け付け、適切な処理を行う。

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
                        Flow.SceneChange(SO_SceneName.Entity.Main, false);
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

                        // ゲーム終了！
                        Flow.QuitGame();
                    }
                }
                // case : クレジット表示ボタンが選択されている場合
                else if (buttonSelecting == ButtonIndex.Credit)
                {
                    // クレジット表示ボタンが選択された状態で、決定が押されたなら...
                    if (InputGetter.Instance.Main_IsSubmit)
                    {
                        // このフレームの後、一切の入力を受け付けない
                        _buttonClicked = true;

                        // クレジットUIを表示する
                        CreditUI.SetActive(true);
                    }
                }
                else
                {
                    throw new Exception("無効なインデックス番号です");
                }

                #endregion
            }
            // ボタンがクリックされているなら...
            else
            {
                // 何もしない！
            }
        }
    }

    // ボタンの状態
    public static class ButtonState
    {
        public static int Normal = 0;
        public static int Select = 1;
    }

    // ボタンの選択状態
    public static class ButtonIndex
    {
        public static int None = 0;
        public static int Start = 1;
        public static int Quit = 2;
        public static int Credit = 3;
    }
}
