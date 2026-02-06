using UnityEngine;

[RequireComponent(typeof(PlayerSpell))]
[RequireComponent(typeof(SwordBuff))]
public class AbilityManager : MonoBehaviour
{

  IAbility spell;
  IAbility buff;
  private PlayerController controller;

  void Awake()
  {
    spell = GetComponent<PlayerSpell>();
    buff = GetComponent<SwordBuff>();
  }

  void Update()
  {
    HandleBuff();
  }

  public void Initialize(PlayerController entity)
  {
    this.controller = entity;
  }

  public void HandleSpell()
  {
    spell.Use(controller);
  }

  public bool CheckSpellCooldown()
  {
    return spell.CanUse();
  }

  void HandleBuff()
  {
    if (!controller.InputReader.BuffSword) return;
    if (buff.CanUse())
      buff.Use(controller);
  }
}