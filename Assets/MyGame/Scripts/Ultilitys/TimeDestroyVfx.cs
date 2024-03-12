using UnityEngine;

public class TimeDestroyVfx : MonoBehaviour
{


      
    void Start()
    {
        Initialized();
    }
    private void Initialized()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            Destroy(gameObject, particleSystem.main.duration);
        }
    }
}
