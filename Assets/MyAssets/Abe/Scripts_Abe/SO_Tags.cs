using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/SO_Tags", fileName = "SO_Tags")]
    public class SO_Tags : ScriptableObject
    {
        #region
        public const string PATH = "SO_Tags";

        private static SO_Tags _entity = null;
        public static SO_Tags Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Tags>(PATH);

                    if (_entity == null)
                    {
                        Debug.LogError(PATH + " not found");
                    }
                }

                return _entity;
            }
        }
        #endregion

        [Header("タグ置き場")]

        [Space(10)]

        [Header("竹")]
        [SerializeField] string bambooTag;
        public string BambooTag => bambooTag;

        [Header("落ち物")]
        [SerializeField] string itemTag;
        public string ItemTag => itemTag;
    }
}