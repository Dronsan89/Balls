using BallsFall.View;
using UnityEngine;

public class DestructionProvider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BallView view))
        {
            view.TakeDamage();
        }
    }
}