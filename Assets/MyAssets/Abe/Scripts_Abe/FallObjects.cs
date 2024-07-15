using General;
using SO;

using UnityEngine;
using Random = UnityEngine.Random;

namespace main
{
    public class FallObjects : MonoBehaviour
    {
        // ���̃v���n�u�̎��
        private enum CType { Rabbit, Arrow, Meteo }
        [SerializeField] CType cType = CType.Rabbit;

        // ���̃C���X�^���X�ɕt����AudioSource���L���b�V������p
        private AudioSource _source = null;

        SO_Tags tags;
        Rigidbody2D myRB;
        float power = 1000;
        bool doOnce = false;
        void Awake()
        {
            myRB = GetComponent<Rigidbody2D>();
            tags = SO_Tags.Entity;
        }

        private void Start()
        {
            // AudioSource��t�^
            _source = gameObject.AddComponent<AudioSource>();

            // �������ɉ����Đ�
            _source.Raise
                (
                    cType switch
                    {
                        CType.Rabbit => General.CType.Rabbit.GetClip(),
                        CType.Arrow => General.CType.Arrow.GetClip(),
                        CType.Meteo => General.CType.Meteo.GetClip(),
                        _ => throw new Exception("�����Ȏ�ނł�")
                    }
                    , SType.SE
                );
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == tags.ItemTag && !doOnce)
            {
                Rigidbody2D hitObjRB = collision.gameObject.GetComponent<Rigidbody2D>();
                doOnce = true;
                //hitObjRB.AddForce(myRB.velocity.normalized * power);
                hitObjRB.AddForce(Random.insideUnitSphere * power);
                //Destroy(gameObject);
            }
        }
    }
}