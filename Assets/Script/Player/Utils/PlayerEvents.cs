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
  public GameEvent OnPlayerHitEnemy;
  public GameEvent OnPlayerHitSound;
  public GameEvent OnFocusBuff;
  public GameEvent OnBuff;
  public GameEvent OnEndBuff;

  public GameEvent OnLaserSpell;
}