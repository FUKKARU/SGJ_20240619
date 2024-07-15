using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.SO;
using Cysharp.Threading.Tasks;
using IA;
using General;
using SO;
using System.Threading;
using System;
using TMPro;

namespace Main
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] int countdownSeconds = 60;
        [SerializeField] GameObject gameClearObj;
        [SerializeField] GameObject gameOverObj;

        
        [NonSerialized] public bool isClear;
        [NonSerialized] public bool isOver;
        float currentSeconds;

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

        #region
        // 現在のターン数(0~)(最大ターン数に達したらゲームオーバー)
        private int _turn = 0;
        private int turn
        {
            get { return _turn; }
            set { _turn = Mathf.Clamp(value, 0, SO_Main.Entity.MaxTurn); }
        }

        // 現在の高度(最大値に達したらクリア)
        private int _height = 0;
        private int height
        {
            get { return _height; }
            set { _height = Mathf.Clamp(value, 0, SO_Main.Entity.MaxHeight); }
        }

        // プレイヤーの番であるかどうか
        private bool _isPlayerTurn = true;
        public bool IsPlayerTurn => _isPlayerTurn;

        // ゲームの結果(0ならまだゲームが終わっていない、1ならクリアになっている、2ならゲームオーバーになっている)
        private int _gameResult = Result.None;
        public int GameResult => _gameResult;

        // UniTaskのキャンセラレーショントークン(このゲームオブジェクトが破棄された際に停止する)
        private CancellationToken _ct;

        

        // 両者が置き終わった後に、必ず呼ばれるべきメソッド(現在の高度を引数に入れること)
        public async void AfterPlace(int heightParam)
        {
            // 【1】まず高度を更新し...
            height = heightParam;

            // 【2】次にクリア判定を行い...(クリアになっているならここで終了)
            if (height >= SO_Main.Entity.MaxHeight)
            {
                _gameResult = Result.Clear;

                // クリアの処理を発火し、ここで処理終了
                await Clear(turn, _ct);
                return;
            }

            // 【3】次にターン数を1増やし...
            turn++;

            // 【4】次にゲームオーバーの処理を行い...(ゲームオーバーになっているならここで終了)
            if (turn >= SO_Main.Entity.MaxTurn)
            {
                _gameResult = Result.Over;

                // ゲームオーバーの処理を発火し、ここで処理終了
                await Over(height, _ct);
                return;
            }

            // 【5】最後に相手のターンにする
            _isPlayerTurn = !_isPlayerTurn;
        }

        // クリアの処理(クリア時のターン数を引数に入れること)
        async UniTask Clear(int turnParam, CancellationToken ct)
        {
            // UI関連の処理(未実装)

            // 一連の処理が終わった後、決定ボタンが押されたらタイトルに戻る
            while (true)
            {
                if (InputGetter.Instance.Main_IsSubmit)
                {
                    Flow.SceneChange(SO_SceneName.Entity.Title, false);
                }

                await UniTask.Yield();
            }
        }

        // ゲームオーバーの処理(ゲームオーバー時の高度を引数に入れること)
        async UniTask Over(int heightParam, CancellationToken ct)
        {
            // UI関連の処理(未実装)

            // 一連の処理が終わった後、決定ボタンが押されたらタイトルに戻る
            while (true)
            {
                if (InputGetter.Instance.Main_IsSubmit)
                {
                    Flow.SceneChange(SO_SceneName.Entity.Title, false);
                }

                await UniTask.Yield();
            }
        }
        #endregion

        private void Start()
        {
            // 時間初期設定
            currentSeconds = countdownSeconds;

            // キャンセラレーショントークンの設定
            _ct = this.GetCancellationTokenOnDestroy();

            // リザルト表示
            gameClearObj.SetActive(false);
            gameOverObj.SetActive(false);
        }

        void Update()
        {
            currentSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)currentSeconds);
            timeText.text = span.ToString(@"mm\:ss");

            if (currentSeconds <= 0) // 制限時間を超えている　かつ　クリアしていない
            {
                GameOver();
            }
        }

        public void GameClear()
        {
            if (isClear || isOver)
            {
                return;
            }

            gameClearObj.SetActive(true);
            isClear = true;
        }

        public void GameOver()
        {
            if (isOver || isClear)
            {
                return;
            }

            gameOverObj.SetActive(true);
            isOver = true;
        }
    }

    // ゲームの結果(0ならまだゲームが終わっていない、1ならクリアになっている、2ならゲームオーバーになっている)
    public static class Result
    {
        public static int None = 0;
        public static int Clear = 1;
        public static int Over = 2;
    }
}