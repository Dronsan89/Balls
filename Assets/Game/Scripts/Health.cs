using BallsFall.Settings;
using System;

namespace BallsFall
{
    public class Health : IDestroyBall
    {
        public event Action<(float, float)> OnChangeHealth;
        public event Action OnDead;

        private float currentHealth;
        private float maxHealth;

        public Health(GameSetting gameSetting) => SetHealth(gameSetting.PlayerHealth, gameSetting.PlayerHealth);

        public void ExtractLife(float damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnDead?.Invoke();
            }

            OnChangeHealth?.Invoke((currentHealth, maxHealth));
        }

        public void ResetGame() => SetHealth(maxHealth, maxHealth);

        public void DestroyBall(DestroyModel model)
        {
            currentHealth -= model.BallModel.Damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnDead?.Invoke();
            }

            OnChangeHealth?.Invoke((currentHealth, maxHealth));
        }

        private void SetHealth(float currentHealth, float maxHealth)
        {
            this.currentHealth = currentHealth;
            this.maxHealth = maxHealth;
            OnChangeHealth?.Invoke((currentHealth, maxHealth));
        }
    }
}
