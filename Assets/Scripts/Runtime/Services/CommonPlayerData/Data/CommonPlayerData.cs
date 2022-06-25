// ReSharper disable FieldCanBeMadeReadOnly.Global

using UnityEngine;

namespace Runtime.Services.CommonPlayerData.Data
{
    public class CommonPlayerData
    {
        public EScene Level;
        public int Money;
        public CommonPlayerData()
        {
            Level = EScene.Level_01;
            Money = 300;
        }
    }
}