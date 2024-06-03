using UnityEngine;

namespace Only1Games.GDBA
{
    [CreateAssetMenu(menuName = "GameDataBase/GameDataBase")]
    public class GameDataBase : ScriptableObject
    {
        public static GameDataBase instance = null;
        public static void InitInstance(GameDataBase gameDataBase)
        {
            instance = gameDataBase;
        }


        [Header("StatusTable")]
        public EnemyBaseStatInfoTable enemyBaseStatInfoTable = null;
        public EnemyGrowthStatInfoTable enemyGrowthStatInfoTable = null;
        public PlayerStatInfoTable playerStatInfoTable = null;
        public ItemStatInfoTable itemStatInfoTable = null;
        public UpgradeStatInfoTable upgradeStatInfoTable = null;
        public ItemBasePriceInfoTable itemBasePriceInfoTable = null;
        public WeaponBasePriceInfoTable weaponBasePriceInfoTable = null;
        public DifficultInfoTable difficultInfoTable = null;
        public WaveStatInfoTable waveStatInfoTable = null;
        public WeaponStatInfoTable weaponStatInfoTable = null;

        [Header("PercentageTable")]
        public WeaponPercentageInfoTable weaponPercentageInfoTable = null;
        public UpgradePercentageInfoTable upgradePercentageInfoTable = null;

        [Header("TextTable")]
        public PlayerSelectTextInfoTable playerSelectTextInfoTable = null;
        public PlayerAchivementInfoTable playerAchivementInfoTable = null;
        public ItemTextInfoTable ItemTextInfoTable = null;
    }


}
