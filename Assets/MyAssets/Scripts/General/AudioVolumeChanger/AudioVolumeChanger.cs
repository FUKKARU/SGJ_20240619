using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace General
{
    public class AudioVolumeChanger : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (IA.InputGetter.Instance.Main_IsUp)
            {
                AudioMixer am = SO_Sound.Entity.AudioMixer;
                am.GetFloat("ParamBGM", out float vol1);
                am.SetFloat("ParamBGM", vol1 + 2);
                am.GetFloat("ParamSE", out float vol2);
                am.SetFloat("ParamSE", vol2 + 2);
            }
            else if (IA.InputGetter.Instance.Main_IsDown)
            {
                AudioMixer am = SO_Sound.Entity.AudioMixer;
                am.GetFloat("ParamBGM", out float vol1);
                am.SetFloat("ParamBGM", vol1 - 2);
                am.GetFloat("ParamSE", out float vol2);
                am.SetFloat("ParamSE", vol2 - 2);
            }
        }
    }
}
