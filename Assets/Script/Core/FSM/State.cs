using UnityEngine;

public abstract class State<T>
{
  protected T entity; // o dono do estado

  public virtual void EnterState(T entity)
  {
    this.entity = entity; // salva a referência
  }

  public virtual void UpdateState(T entity) { }

  public virtual void FixedUpdateState(T entity) { }

  public virtual void ExitState(T entity) { }
}