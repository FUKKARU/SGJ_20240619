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
        /// // SEの種類に対応したAudioClipのリストの中から、ランダムに1つ選択して返す
        /// </summary>
        /// <param name="cType">SEの種類</param>
        /// <param name="index">indexが-1ならランダム、そうでないならそのインデックス番号の物を取得する(範囲外にならないように注意)</param>
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
                    _ => throw new Exception("無効な種類です")
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
                    _ => throw new Exception("無効な種類です")
                };
            }
        }

        // リストの中からランダムに1つ選択
        public static T RChoice<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }

    // SEの種類
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
