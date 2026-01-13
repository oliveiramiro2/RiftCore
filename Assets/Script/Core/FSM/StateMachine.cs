using System;
using System.Collections.Generic;

public class StateMachine<T>
{
  private T owner;
  public State<T> CurrentState { get; private set; }

  private Dictionary<State<T>, List<Transition>> transitions =
      new Dictionary<State<T>, List<Transition>>();

  private List<Transition> anyTransitions = new List<Transition>();

  public StateMachine(T owner)
  {
    this.owner = owner;
  }

  public void Initialize(State<T> startState)
  {
    CurrentState = startState;
    CurrentState.EnterState(owner);
  }

  public void Update()
  {
    // ANY-state transitions
    foreach (var t in anyTransitions)
    {
      if (t.Condition())
      {
        ChangeState(t.To);
        return;
      }
    }

    // Specific transitions
    if (transitions.TryGetValue(CurrentState, out var list))
    {
      foreach (var t in list)
      {
        if (t.Condition())
        {
          ChangeState(t.To);
          return;
        }
      }
    }

    CurrentState.UpdateState(owner);
  }

  public void FixedUpdate()
  {
    CurrentState.FixedUpdateState(owner);
  }

  public void ChangeState(State<T> newState)
  {
    if (newState == CurrentState) return;

    CurrentState.ExitState(owner);
    CurrentState = newState;
    CurrentState.EnterState(owner);
  }

  public void AddTransition(State<T> from, State<T> to, Func<bool> condition)
  {
    if (from == null)
      throw new ArgumentNullException(nameof(from), "From state cannot be null.");
    if (!transitions.ContainsKey(from))
      transitions[from] = new List<Transition>();

    transitions[from].Add(new Transition(to, condition));
  }

  public void AddAnyTransition(State<T> to, Func<bool> condition)
  {
    anyTransitions.Add(new Transition(to, condition));
  }

  private class Transition
  {
    public State<T> To;
    public Func<bool> Condition;

    public Transition(State<T> to, Func<bool> condition)
    {
      To = to;
      Condition = condition;
    }
  }
}
