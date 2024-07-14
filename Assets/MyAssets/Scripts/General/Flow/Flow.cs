using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine.SceneManagement;

namespace General
{
    public static class Flow
    {
        // シーン遷移(durは、この処理が発火された後に待つ秒数)
        public static async UniTask SceneChange(string toSceneName, bool isAsync, float dur, CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(dur));

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

        // ゲーム終了(durは、この処理が発火された後に待つ秒数)
        public static async UniTask QuitGame(float dur, CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(dur));

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
