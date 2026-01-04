using System;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
  private StateMachine<PlayerController> sm;
  private PlayerController owner;
  private bool initialized = false;

  public State<PlayerController> CurrentState => sm == null ? null : sm.CurrentState;

  public void Setup(PlayerController controller)
  {
    this.owner = controller;
    this.sm = new StateMachine<PlayerController>(owner);
  }

  public void Initialize(State<PlayerController> startingState)
  {
    // Proteção extra
    if (sm == null)
    {
      Debug.LogError("StateMachine não foi criada! Chame Setup() antes.");
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

  public void ChangeState(State<PlayerController> newState)
  {
    sm.ChangeState(newState);
  }

  public void AddTransition(State<PlayerController> from, State<PlayerController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddTransition(from, to, condition);
  }

  public void AddAnyTransition(State<PlayerController> to, Func<bool> condition)
  {
    if (sm != null)
      sm.AddAnyTransition(to, condition);
  }
}