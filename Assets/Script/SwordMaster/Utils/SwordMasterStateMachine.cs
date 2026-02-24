using System;
using UnityEngine;

public class SwordMasterStateMachine : MonoBehaviour
{
  private StateMachine<SwordMasterController> sm;
  private SwordMasterController owner;
  private bool initialized = false;

  public State<SwordMasterController> CurrentState => sm == null ? null : sm.CurrentState;
  public void Setup(SwordMasterController controller)
  {
    this.owner = controller;
    this.sm = new StateMachine<SwordMasterController>(owner);
  }

  public void Initialize(State<SwordMasterController> startingState)
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

  public void ChangeState(State<SwordMasterController> newState)
  {
    sm.ChangeState(newState);
  }

  public void AddTransition(State<SwordMasterController> from, State<SwordMasterController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddTransition(from, to, condition);
  }

  public void AddAnyTransition(State<SwordMasterController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddAnyTransition(to, condition);
  }
}