using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerManager : MonoBehaviour
{
  private static TimerManager _instance;
  public static TimerManager Instance
  {
    get
    {
      if (_instance == null)
      {
        var obj = new GameObject("TimerManager");
        _instance = obj.AddComponent<TimerManager>();
      }
      return _instance;
    }
  }

  private List<Coroutine> activeTimers = new List<Coroutine>();

  public Coroutine StartTimer(float duration, Action onComplete)
  {
    Coroutine c = StartCoroutine(RunTimer(duration, onComplete));
    activeTimers.Add(c);
    return c;
  }

  private IEnumerator RunTimer(float duration, Action onComplete)
  {
    yield return new WaitForSeconds(duration);
    onComplete?.Invoke();
  }

  public void StopTimer(Coroutine timer)
  {
    if (timer != null)
    {
      StopCoroutine(timer);
      activeTimers.Remove(timer);
    }
  }

  public void StopAllTimers()
  {
    foreach (var timer in activeTimers)
      StopCoroutine(timer);
    activeTimers.Clear();
  }
}
