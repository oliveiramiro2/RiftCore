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
    HandleSpell();
    HandleBuff();
  }

  public void Initialize(PlayerController entity)
  {
    this.controller = entity;
  }

  void HandleSpell()
  {
    if (!controller.InputReader.CastSpell) return;
    if (spell.CanUse())
      spell.Use(controller);
  }

  void HandleBuff()
  {
    if (!controller.InputReader.BuffSword) return;
    if (buff.CanUse())
      buff.Use(controller);
  }
}