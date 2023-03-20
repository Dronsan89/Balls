using UnityEngine;

public class ParticleFx : MonoBehaviour
{
    public void Init(Color color)
    {
        var particleSystem = GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.startColor = color;
        Destroy(gameObject, main.startLifetimeMultiplier);
    }
}
