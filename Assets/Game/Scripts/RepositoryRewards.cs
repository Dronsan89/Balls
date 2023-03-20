using System;

namespace BallsFall
{
    public class RepositoryRewards : IDestroyBall
    {
        public event Action<int> OnChangeRewards;

        private int currentReward;

        public void ResetCurrentReward()
        {
            currentReward = 0;
            OnChangeRewards?.Invoke(currentReward);
        }

        public void DestroyBall(DestroyModel model)
        {
            currentReward += model.BallModel.Reward;
            OnChangeRewards?.Invoke(currentReward);
        }
    }
}
