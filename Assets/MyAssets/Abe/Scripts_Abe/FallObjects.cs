using General;
using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace main
{
    public class FallObjects : MonoBehaviour
    {
        // このプレハブの種類
        private enum CType { Rabbit, Arrow, Meteo }
        [SerializeField] CType cType = CType.Rabbit;

        // このインスタンスに付けたAudioSourceをキャッシュする用
        private AudioSource _source = null;

        SO_Tags tags;
        Rigidbody2D myRB;
        float power = 1000;
        void Awake()
        {
            myRB = GetComponent<Rigidbody2D>();
            tags = SO_Tags.Entity;
        }

        private void Start()
        {
            // AudioSourceを付与
            _source = gameObject.AddComponent<AudioSource>();

            // 生成時に音を再生
            _source.Raise
                (
                    cType switch
                    {
                        CType.Rabbit => General.CType.Rabbit.GetClip(),
                        CType.Arrow => General.CType.Rabbit.GetClip(),
                        CType.Meteo => General.CType.Rabbit.GetClip(),
                        _ => throw new Exception("無効な種類です")
                    }
                    , SType.SE
                );
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == tags.ItemTag)
            {
                Rigidbody2D hitObjRB = collision.gameObject.GetComponent<Rigidbody2D>();
                //hitObjRB.AddForce(myRB.velocity.normalized * power);
                hitObjRB.AddForce(Random.insideUnitSphere * power);
                //Destroy(gameObject);
            }
        }
    }
}