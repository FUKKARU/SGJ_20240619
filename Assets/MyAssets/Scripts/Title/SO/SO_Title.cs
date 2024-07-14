using UnityEngine;

namespace Title.SO
{
    [CreateAssetMenu(menuName = "SO/SO_Title", fileName = "SO_Title")]
    public class SO_Title : ScriptableObject
    {
        #region
        public const string PATH = "SO_Title";

        private static SO_Title _entity = null;
        public static SO_Title Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Title>(PATH);

                    if (_entity == null)
                    {
                        Debug.LogError(PATH + " not found");
                    }
                }

                return _entity;
            }
        }
        #endregion

        [Header("É{É^ÉìÇâüÇµÇΩå„ÅAâΩïbë“Ç¬Ç©")] public float OnButtonClickWaitDur;
    }
}