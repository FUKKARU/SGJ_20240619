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

        [Header("最大ターン数(0~)\n(自分と敵のの合計)")] public int MaxTurn;
        [Header("プレイヤーが、1ターンで何本設置できるか")] public int PlaceNumPerTurn;
        [Header("プレイヤーの保持している竹(など)が、\n何秒で強制的に落下するか")] public int HoldLimit;
        [Header("最大高度(ここまで詰めたらクリア)\n(単位は [km] )")] public int MaxHeight;
    }
}