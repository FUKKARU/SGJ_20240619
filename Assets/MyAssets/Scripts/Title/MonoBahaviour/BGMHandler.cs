using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using SO;

namespace Title
{
    public class BGMHandler : MonoBehaviour
    {
        // タイトルBGMのAudioSource(このゲームオブジェクトに付いている想定)
        AudioSource _source;

        private void Start()
        {
            // シーン開始時にBGMを再生
            _source = GetComponent<AudioSource>();
            _source.Raise(SO_Sound.Entity.TitleBGM, SType.BGM);
        }

        private void Update()
        {

        }
    }
}