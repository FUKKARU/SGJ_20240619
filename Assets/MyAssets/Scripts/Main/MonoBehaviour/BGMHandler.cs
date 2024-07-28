using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using SO;

namespace Main
{
    public class BGMHandler : MonoBehaviour
    {
        // �Q�[����BGM��AudioSource(���̃Q�[���I�u�W�F�N�g�ɕt���Ă���z��)
        AudioSource _source;

        private void Start()
        {
            // �V�[���J�n����BGM���Đ�
            _source = GetComponent<AudioSource>();
            _source.Raise(SO_Sound.Entity.MainBGM, SType.BGM);
            _source.volume =0.2f;
        }

        private void Update()
        {

        }
    }
}