using System;
using UnityEngine;

public class MawStateMachine : MonoBehaviour
{
  private StateMachine<MawController> sm;
  private MawController owner;
  private bool initialized = false;

  public State<MawController> CurrentState => sm == null ? null : sm.CurrentState;
  public void Setup(MawController controller)
  {
    this.owner = controller;
    this.sm = new StateMachine<MawController>(owner);
  }

  public void Initialize(State<MawController> startingState)
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

  public void ChangeState(State<MawController> newState)
  {
    sm.ChangeState(newState);
  }

  public void AddTransition(State<MawController> from, State<MawController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddTransition(from, to, condition);
  }

  public void AddAnyTransition(State<MawController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddAnyTransition(to, condition);
  }
}