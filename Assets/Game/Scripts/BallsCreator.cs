using BallsFall.Settings;
using BallsFall.View;
using Extensions;
using System;
using System.Collections;
using UnityEngine;

namespace BallsFall
{
    public class BallsCreator : MonoBehaviour
    {
        public event Action<DestroyModel, IDestroyBall> OnDestroyBall;

        [Header("Pool")]
        [SerializeField] private BallView prefabBall;
        [SerializeField] private int poolCount = 15;
        [SerializeField] private bool autoExpand = true;

        private Pool<BallView> poolBalls;

        private GameSetting gameSetting;
        private IEnumerator spawnLoop;
        private Health health;
        private RepositoryRewards rewards;

        public void Init(GameSetting gameSetting, Health health, RepositoryRewards rewards)
        {
            this.gameSetting = gameSetting;
            this.health = health;
            this.rewards = rewards;

            poolBalls = new Pool<BallView>(prefabBall, poolCount, transform, autoExpand);
        }

        public void Enable()
        {
            spawnLoop = Spawner();
            StartCoroutine(spawnLoop);
        }

        public void Disable() => StopAllCoroutines();

        public void DestroyAllBall() => poolBalls.DiactivateAll();

        private IEnumerator Spawner()
        {
            while (true)
            {
                yield return new WaitForSeconds(gameSetting.CooldownCreationBalls);
                CreateBall();
            }
        }

        private void CreateBall()
        {
            var position = new Vector2(new Vector2(gameSetting.ScreenBorder.LeftBorder, gameSetting.ScreenBorder.RightBorder).GetRandomRange(),
                new Vector2(gameSetting.ScreenBorder.UpBorder, gameSetting.ScreenBorder.DownBorder).GetRandomRange());

            var ballView = poolBalls.GetFreeElement();

            ballView.transform.localScale = new Vector3(gameSetting.BallSize, gameSetting.BallSize, gameSetting.BallSize);
            ballView.Init(gameSetting, health, rewards, position);
            ballView.OnDestroy += ControllerDestroy;
        }

        private void ControllerDestroy(DestroyModel model, IDestroyBall destroyBall)
        {
            OnDestroyBall?.Invoke(model, destroyBall);
            model.BallView.OnDestroy -= ControllerDestroy;
        }
    }
}
