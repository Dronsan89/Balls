using BallsFall.Settings;
using System;
using UnityEngine;

namespace BallsFall
{
    public class ProgressTimer : MonoBehaviour
    {
        public event Action<float> OnSpeedUp;

        public bool IsEnable { get; set; }

        private GameSetting gameSetting;
        private float timer;

        public void Init(GameSetting gameSetting)
        {
            this.gameSetting = gameSetting;
            ResetGame();
        }

        private void Update()
        {
            if (IsEnable)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    UpLevelProgress(gameSetting.AddedSpeedStep);
                }
            }
        }

        public void ResetGame()
        {
            gameSetting.AddedSpeed = 0;
            timer = gameSetting.UpLevelTime;
        }

        private void UpLevelProgress(float addedSpeedStep)
        {
            gameSetting.AddedSpeed += addedSpeedStep;
            timer = gameSetting.UpLevelTime;
            OnSpeedUp?.Invoke(gameSetting.AddedSpeed);
        }
    }
}
