using UnityEngine;
using UnityEngine.Audio;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/SO_Sound", fileName = "SO_Sound")]
    public class SO_Sound : ScriptableObject
    {
        #region
        public const string PATH = "SO_Sound";

        private static SO_Sound _entity = null;
        public static SO_Sound Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Sound>(PATH);

                    if (_entity == null)
                    {
                        Debug.LogError(PATH + " not found");
                    }
                }

                return _entity;
            }
        }
        #endregion

        [Header("生成するAudioSourceのプレハブ")] public GameObject ASPrefab;
        [Header("MasterのAudioMixerGroup")] public AudioMixerGroup AMGroupMaster;
        [Header("BGMのAudioMixerGroup")] public AudioMixerGroup AMGroupBGM;
        [Header("SEのAudioMixerGroup")] public AudioMixerGroup AMGroupSE;
        [Space(50)]
        [Header("BGM")]
        [Header("タイトル")] public AudioClip TitleBGM;
        [Space(50)]
        [Header("SE")]
        [Header("クリック")] public AudioClip ClickSE;
    }
}