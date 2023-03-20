using BallsFall.Controller;
using BallsFall.Model;
using BallsFall.Settings;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using Extensions;

namespace BallsFall.View
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class BallView : MonoBehaviour, IPointerDownHandler
    {
        public event Action<DestroyModel, IDestroyBall> OnDestroy;

        [SerializeField] private ParticleFx fxPrefab;

        private SpriteRenderer spriteRenderer;
        private BallModel ballModel;
        private BallController ballController;
        private Health health;
        private RepositoryRewards rewards;

        public void Init(GameSetting gameSetting, Health health, RepositoryRewards rewards, Vector3 position)
        {
            int reward = (int)new Vector2(gameSetting.RangeRewardValuesMinMax.x, gameSetting.RangeRewardValuesMinMax.y).GetRandomRange();
            float speed = new Vector2(gameSetting.RangeSpeedValuesMinMax.x + gameSetting.AddedSpeed, gameSetting.RangeSpeedValuesMinMax.y + gameSetting.AddedSpeed).GetRandomRange();
            float damage = new Vector2(gameSetting.RangeDamageValuesMinMax.x, gameSetting.RangeDamageValuesMinMax.y).GetRandomRange();

            ballModel = new BallModel(reward, GetRandomColor(), speed, damage, position);

            ballController = new BallController(ballModel);
            spriteRenderer = GetComponent<SpriteRenderer>();

            this.health = health;
            this.rewards = rewards;

            Settings(ballModel);
        }

        private void Update()
        {
            ballController.UpdatePosition(Time.deltaTime);
            transform.position = ballModel.Position;
        }

        public void OnPointerDown(PointerEventData eventData) => DestroyBall(StateDestroyable.reward, rewards);

        public void TakeDamage() => DestroyBall(StateDestroyable.damage, health);

        private void Settings(BallModel model)
        {
            ballModel = model;
            spriteRenderer.color = ballModel.Color;
        }

        private void DestroyBall(StateDestroyable state, IDestroyBall destroyBall)
        {
            OnDestroy?.Invoke(new DestroyModel(this, state, ballModel), destroyBall);
            CreateFx();
            DestroyThis();
        }

        private void CreateFx()
        {
            var particle = Instantiate(fxPrefab, transform.position, Quaternion.identity);

            particle.Init(ballModel.Color);
        }

        private void DestroyThis()
        {
            gameObject.SetActive(false);
        }

        private Color GetRandomColor() => Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
