using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using SO;

namespace Title
{
    public class BGMHandler : MonoBehaviour
    {
        // �^�C�g��BGM��AudioSource(���̃Q�[���I�u�W�F�N�g�ɕt���Ă���z��)
        AudioSource _source;

        private void Start()
        {
            // �V�[���J�n����BGM���Đ�
            _source = GetComponent<AudioSource>();
            _source.Raise(SO_Sound.Entity.TitleBGM, SType.BGM);
        }

        private void Update()
        {

        }
    }
}