using UnityEngine;

public class AWFXManager : MonoBehaviour
{
    public ParticleSystem phase2Effect;

    public void PlayPhase2Effect()
    {
        phase2Effect.Play();
    }
}
