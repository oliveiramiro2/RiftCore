using System;
using UnityEngine;

public class AstralWeaverStateMachine : MonoBehaviour
{
  private StateMachine<AstralWeaverController> sm;
  private AstralWeaverController owner;
  private bool initialized = false;

  public State<AstralWeaverController> CurrentState => sm == null ? null : sm.CurrentState;

  public void Setup(AstralWeaverController controller)
  {
    this.owner = controller;
    this.sm = new StateMachine<AstralWeaverController>(owner);
  }

  public void Initialize(State<AstralWeaverController> startingState)
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

  public void ChangeState(State<AstralWeaverController> newState)
  {
    sm.ChangeState(newState);
  }

  public void AddTransition(State<AstralWeaverController> from, State<AstralWeaverController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddTransition(from, to, condition);
  }

  public void AddAnyTransition(State<AstralWeaverController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddAnyTransition(to, condition);
  }
}