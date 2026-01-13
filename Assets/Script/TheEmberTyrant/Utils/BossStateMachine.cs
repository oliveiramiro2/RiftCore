using System;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
  private StateMachine<BossController> sm;
  private BossController owner;
  private bool initialized = false;

  public State<BossController> CurrentState => sm == null ? null : sm.CurrentState;

  public void Setup(BossController controller)
  {
    this.owner = controller;
    this.sm = new StateMachine<BossController>(owner);
  }

  public void Initialize(State<BossController> startingState)
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

  public void ChangeState(State<BossController> newState)
  {
    sm.ChangeState(newState);
  }

  public void AddTransition(State<BossController> from, State<BossController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddTransition(from, to, condition);
  }

  public void AddAnyTransition(State<BossController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddAnyTransition(to, condition);
  }
}