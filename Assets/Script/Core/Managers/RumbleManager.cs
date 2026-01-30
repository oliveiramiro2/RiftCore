using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager Instance;

    Coroutine rumbleRoutine;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Play(RumbleType type)
    {
        if (Gamepad.current == null)
            return;

        if (rumbleRoutine != null)
            StopCoroutine(rumbleRoutine);

        rumbleRoutine = StartCoroutine(RumbleRoutine(type));
    }

    public void Stop()
    {
        if (Gamepad.current == null)
            return;

        if (rumbleRoutine != null)
            StopCoroutine(rumbleRoutine);

        Gamepad.current.SetMotorSpeeds(0f, 0f);
        rumbleRoutine = null;
    }

    IEnumerator RumbleRoutine(RumbleType type)
    {
        switch (type)
        {
            case RumbleType.LightHit:
                yield return SimpleRumble(0.1f, 0.3f, 0.05f);
                break;

            case RumbleType.HeavyHit:
                yield return SimpleRumble(0.6f, 0.8f, 0.15f);
                break;

            case RumbleType.Slam:
                yield return SimpleRumble(0.8f, 0.2f, 0.2f);
                break;

            case RumbleType.Charge:
                yield return ChargeRumble(0.5f);
                break;

            case RumbleType.Danger:
                yield return PulseRumble(0.3f, 3);
                break;
        }

        Stop();
    }

    IEnumerator SimpleRumble(float low, float high, float duration)
    {
        Gamepad.current.SetMotorSpeeds(low, high);
        yield return new WaitForSeconds(duration);
    }

    IEnumerator ChargeRumble(float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float strength = Mathf.Lerp(0.1f, 0.6f, t / duration);
            Gamepad.current.SetMotorSpeeds(strength, strength);
            yield return null;
        }
    }

    IEnumerator PulseRumble(float strength, int pulses)
    {
        for (int i = 0; i < pulses; i++)
        {
            Gamepad.current.SetMotorSpeeds(strength, strength);
            yield return new WaitForSeconds(0.08f);
            Gamepad.current.SetMotorSpeeds(0f, 0f);
            yield return new WaitForSeconds(0.08f);
        }
    }

    void OnDisable()
    {
        Stop();
    }
}


public enum RumbleType
{
    LightHit,
    HeavyHit,
    Slam,
    Charge,
    Danger
}
