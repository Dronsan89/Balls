using System;
using UnityEngine;

namespace BallsFall.Settings
{
    [Serializable]
    public struct ScreenModel
    {
        public float LeftBorder;
        public float RightBorder;
        public float UpBorder;
        public float DownBorder;
    }

    [CreateAssetMenu(fileName = "SettingsGame", menuName = "SettingName", order = 0)]
    public class GameSetting : ScriptableObject
    {
        public Vector2 RangeRewardValuesMinMax;
        public Vector2 RangeDamageValuesMinMax;
        public Vector2 RangeSpeedValuesMinMax;
        public float AddedSpeedStep;
        public float UpLevelTime;
        public float CooldownCreationBalls;
        [Range(0.1f, 5)] public float BallSize;
        public ScreenModel ScreenBorder;
        public float AddedSpeed;
        public float PlayerHealth;
    }
}
