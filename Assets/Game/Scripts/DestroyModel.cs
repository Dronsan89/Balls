using BallsFall.Model;
using BallsFall.View;

namespace BallsFall
{
    public enum StateDestroyable
    {
        damage,
        reward,
    }

    public class DestroyModel
    {
        public BallView BallView { get; private set; }
        public StateDestroyable StateDestroyable { get; private set; }
        public BallModel BallModel { get; private set; }

        public DestroyModel(BallView ballView, StateDestroyable stateDestroyable, BallModel ballModel)
        {
            BallView = ballView;
            StateDestroyable = stateDestroyable;
            BallModel = ballModel;
        }
    }
}
