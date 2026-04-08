using System;
using UnityEngine;

public class LasterStateMachine : MonoBehaviour
{
  private StateMachine<LasterController> sm;
  private LasterController owner;
  private bool initialized = false;

  public State<LasterController> CurrentState => sm == null ? null : sm.CurrentState;
  public void Setup(LasterController controller)
  {
    this.owner = controller;
    this.sm = new StateMachine<LasterController>(owner);
  }

  public void Initialize(State<LasterController> startingState)
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

  public void ChangeState(State<LasterController> newState)
  {
    sm.ChangeState(newState);
  }

  public void AddTransition(State<LasterController> from, State<LasterController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddTransition(from, to, condition);
  }

  public void AddAnyTransition(State<LasterController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddAnyTransition(to, condition);
  }
}