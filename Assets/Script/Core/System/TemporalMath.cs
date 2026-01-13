using UnityEngine;

public static class TemporalMath
{
  public static float Lerp(float current, float target, float speed)
  {
    return Mathf.Lerp(current, target, speed * Time.deltaTime);
  }

  public static float LerpWithPulse(
      float current,
      float target,
      float speed,
      float pulseAmplitude,
      float pulseFrequency
  )
  {
    float lerped = Mathf.Lerp(current, target, speed * Time.deltaTime);
    float pulse = Mathf.Sin(Time.time * pulseFrequency) * pulseAmplitude;
    return lerped + pulse;
  }

  public static float SinPulse(float amplitude, float frequency)
  {
    return Mathf.Sin(Time.time * frequency) * amplitude;
  }

  public static float PingPong(float min, float max, float speed)
  {
    float t = Mathf.PingPong(Time.time * speed, 1f);
    return Mathf.Lerp(min, max, t);
  }

  public static float SmoothStep(float current, float target, float speed)
  {
    float t = speed * Time.deltaTime;
    t = t * t * (3f - 2f * t);
    return Mathf.Lerp(current, target, t);
  }

  public static float EaseIn(float t) => t * t;
  public static float EaseOut(float t) => 1f - Mathf.Pow(1f - t, 2f);
  public static float EaseInOut(float t)
  {
    return t < 0.5f
        ? 2f * t * t
        : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;
  }
}
