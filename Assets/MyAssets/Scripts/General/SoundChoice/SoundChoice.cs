using System;
using System.Collections.Generic;
using UnityEngine;
using SO;
using Random = UnityEngine.Random;

namespace General
{
    public static class SoundChoice
    {
        /// <summary>
        /// // SE�̎�ނɑΉ�����AudioClip�̃��X�g�̒�����A�����_����1�I�����ĕԂ�
        /// </summary>
        /// <param name="cType">SE�̎��</param>
        /// <param name="index">index��-1�Ȃ烉���_���A�����łȂ��Ȃ炻�̃C���f�b�N�X�ԍ��̕����擾����(�͈͊O�ɂȂ�Ȃ��悤�ɒ���)</param>
        /// <returns>AudioClip</returns>
        public static AudioClip GetClip(this CType cType, int index = -1)
        {
            if (index == -1)
            {
                return cType switch
                {
                    CType.Mikado => SO_Sound.Entity.MikadoSE.RChoice(),
                    CType.Okina => SO_Sound.Entity.OkinaSE.RChoice(),
                    CType.Ohna => SO_Sound.Entity.OhnaSE.RChoice(),
                    CType.Rabbit => SO_Sound.Entity.RabbitSE.RChoice(),
                    CType.Arrow => SO_Sound.Entity.ArrowSE.RChoice(),
                    CType.Meteo => SO_Sound.Entity.MeteoSE.RChoice(),
                    CType.Bamboo => SO_Sound.Entity.BambooSE.RChoice(),
                    _ => throw new Exception("�����Ȏ�ނł�")
                };
            }
            else
            {
                return cType switch
                {
                    CType.Mikado => SO_Sound.Entity.MikadoSE[index],
                    CType.Okina => SO_Sound.Entity.OkinaSE[index],
                    CType.Ohna => SO_Sound.Entity.OhnaSE[index],
                    CType.Rabbit => SO_Sound.Entity.RabbitSE[index],
                    CType.Arrow => SO_Sound.Entity.ArrowSE[index],
                    CType.Meteo => SO_Sound.Entity.MeteoSE[index],
                    CType.Bamboo => SO_Sound.Entity.BambooSE[index],
                    _ => throw new Exception("�����Ȏ�ނł�")
                };
            }
        }

        // ���X�g�̒����烉���_����1�I��
        public static T RChoice<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }

    // SE�̎��
    public enum CType
    {
        Mikado,
        Okina,
        Ohna,
        Rabbit,
        Arrow,
        Meteo,
        Bamboo
    }
}
