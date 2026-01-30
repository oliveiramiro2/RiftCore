using System;
using UnityEngine;

public class ToxicSlimeStateMachine : MonoBehaviour
{
  private StateMachine<ToxicSlimeController> sm;
  private ToxicSlimeController owner;
  private bool initialized = false;

  public State<ToxicSlimeController> CurrentState => sm == null ? null : sm.CurrentState;
  public void Setup(ToxicSlimeController controller)
  {
    this.owner = controller;
    this.sm = new StateMachine<ToxicSlimeController>(owner);
  }

  public void Initialize(State<ToxicSlimeController> startingState)
  {
    if (sm == null)
    {
      return;
    }

    sm.Initialize(startingState);
    initialized = true;
  }

  public void UpdateStateMachine()
  {
    if (!initialized) return;
    sm.Update();
  }

  public void FixedUpdateStateMachine()
  {
    if (!initialized) return;
    sm.FixedUpdate();
  }

  public void ChangeState(State<ToxicSlimeController> newState)
  {
    sm.ChangeState(newState);
  }

  public void AddTransition(State<ToxicSlimeController> from, State<ToxicSlimeController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddTransition(from, to, condition);
  }

  public void AddAnyTransition(State<ToxicSlimeController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddAnyTransition(to, condition);
  }
}