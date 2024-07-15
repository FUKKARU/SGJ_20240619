using UnityEngine;

namespace Main.SO
{
    [CreateAssetMenu(menuName = "SO/SO_Main", fileName = "SO_Main")]
    public class SO_Main : ScriptableObject
    {
        #region
        public const string PATH = "SO_Main";

        private static SO_Main _entity = null;
        public static SO_Main Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Main>(PATH);

                    if (_entity == null)
                    {
                        Debug.LogError(PATH + " not found");
                    }
                }

                return _entity;
            }
        }
        #endregion

        [Header("�ő�^�[����(0~)\n(�����ƓG�̂̍��v)")] public int MaxTurn;
        [Header("�v���C���[���A1�^�[���ŉ��{�ݒu�ł��邩")] public int PlaceNumPerTurn;
        [Header("�v���C���[�̕ێ����Ă���|(�Ȃ�)���A\n���b�ŋ����I�ɗ������邩")] public int HoldLimit;
        [Header("�ő卂�x(�����܂ŋl�߂���N���A)\n(�P�ʂ� [km] )")] public int MaxHeight;
    }
}