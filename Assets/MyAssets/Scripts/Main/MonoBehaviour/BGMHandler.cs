using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using SO;

namespace Main
{
    public class BGMHandler : MonoBehaviour
    {
        // ゲーム内BGMのAudioSource(このゲームオブジェクトに付いている想定)
        AudioSource _source;

        private void Start()
        {
            // シーン開始時にBGMを再生
            _source = GetComponent<AudioSource>();
            _source.Raise(SO_Sound.Entity.MainBGM, SType.BGM);
            _source.volume =0.2f;
        }

        private void Update()
        {

        }
    }
}