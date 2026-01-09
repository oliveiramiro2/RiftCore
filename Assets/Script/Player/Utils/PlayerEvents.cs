using UnityEngine;

[CreateAssetMenu(menuName = "Events/Controllers/PlayerEvents", fileName = "PlayerEvents")]
public class PlayerEvents : ScriptableObject
{

  public GameEvent OnAttack;
  public GameEvent OnJump;
  public GameEvent OnMove;
  public GameEvent OnLand;

  public GameEvent OnDash;
  public GameEvent OnTakeDamage;
  public GameEvent OnDeath;
}