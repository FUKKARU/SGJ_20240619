using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine.SceneManagement;

namespace General
{
    public static class Flow
    {
        // �V�[���J��(dur�́A���̏��������΂��ꂽ��ɑ҂b��)
        public static async UniTask SceneChange(string toSceneName, bool isAsync, float dur, CancellationToken ct)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(dur));

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

        // �Q�[���I��(dur�́A���̏��������΂��ꂽ��ɑ҂b��)
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
