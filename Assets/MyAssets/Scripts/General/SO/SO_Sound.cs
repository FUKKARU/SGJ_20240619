using System.Collections.Generic;
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

        [Header("MasterのAudioMixerGroup")] public AudioMixerGroup AMGroupMaster;
        [Header("BGMのAudioMixerGroup")] public AudioMixerGroup AMGroupBGM;
        [Header("SEのAudioMixerGroup")] public AudioMixerGroup AMGroupSE;
        [Space(50)]
        [Header("BGM")]
        [Header("タイトル")] public AudioClip TitleBGM;
        [Header("ゲーム内")] public AudioClip MainBGM;
        [Space(50)]
        [Header("SE")]
        [Header("矢が飛んでくる(ランダム)")] public List<AudioClip> ArrowSE;
        [Header("隕石が飛んでくる(ランダム)")] public List<AudioClip> MeteoSE;
        [Header("竹が初めてぶつかった(ランダム)")] public List<AudioClip> BambooSE;
        [Header("帝が初めてぶつかった(ランダム)")] public List<AudioClip> MikadoSE;
        [Header("翁が初めてぶつかった(ランダム)")] public List<AudioClip> OkinaSE;
        [Header("媼が初めてぶつかった(ランダム)")] public List<AudioClip> OhnaSE;
        [Header("ウサギが飛んでくる(ランダム)")] public List<AudioClip> RabbitSE;
    }
}