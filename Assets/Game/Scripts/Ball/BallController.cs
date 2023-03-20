using BallsFall.Model;
using UnityEngine;

namespace BallsFall.Controller
{
    public class BallController
    {
        private BallModel ballModel;

        public BallController(BallModel model) => ballModel = model;

        public void UpdatePosition(float deltaTime) => ballModel.Position += Vector3.down * ballModel.Speed * deltaTime;
    }
}
