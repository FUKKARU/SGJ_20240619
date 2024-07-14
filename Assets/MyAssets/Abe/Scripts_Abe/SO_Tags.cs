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

        [Header("ƒ^ƒO’u‚«ê")]

        [Space(10)]

        [Header("’|")]
        [SerializeField] string bambooTag;
        public string BambooTag => bambooTag;
    }
}