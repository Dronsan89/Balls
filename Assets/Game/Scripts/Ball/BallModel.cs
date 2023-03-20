using UnityEngine;

namespace BallsFall.Model
{
    public class BallModel
    {
        public int Reward { get; private set; }
        public Color Color { get; private set; }
        public float Speed { get; private set; }
        public float Damage { get; private set; }
        public Vector3 Position { get; set; }

        public BallModel(int reward, Color color, float speed, float damage, Vector3 position)
        {
            Reward = reward;
            Color = color;
            Speed = speed;
            Damage = damage;
            Position = position;
        }
    }
}
