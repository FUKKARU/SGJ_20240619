using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine.SceneManagement;

namespace General
{
    public static class Flow
    {
        // ƒV[ƒ“‘JˆÚ(dur‚ÍA‚±‚Ìˆ—‚ª”­‰Î‚³‚ê‚½Œã‚É‘Ò‚Â•b”)
        public static async UniTask SceneChange(string toSceneName, bool isAsync, float dur, CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(dur));

            // ”ñ“¯Šú‘JˆÚ
            if (isAsync)
            {
                // ‚Ü‚¾–¢À‘•
            }
            // ‘¦‘JˆÚ
            else
            {
                SceneManager.LoadScene(toSceneName);
            }
        }

        // ƒQ[ƒ€I—¹(dur‚ÍA‚±‚Ìˆ—‚ª”­‰Î‚³‚ê‚½Œã‚É‘Ò‚Â•b”)
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
