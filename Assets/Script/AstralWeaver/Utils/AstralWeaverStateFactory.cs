using UnityEngine;

public class AstralWeaverStateFactory : MonoBehaviour
{
  [HideInInspector] public AstralWeaverStateMachine bossSM;
  [HideInInspector] public AstralWeaverController owner;
  [HideInInspector] public AstralWeaverAnimationBridge animatorBridge;

  public bool isIdle = true;


  public AstralWeaverIdle AstralWeaverIdle { get; private set; }
  public AstralWeaverAttacks AstralWeaverAttacks { get; private set; }

  void Awake()
  {
    AstralWeaverIdle = new AstralWeaverIdle();
    AstralWeaverAttacks = new AstralWeaverAttacks();
  }

  public void InitializeTransitions()
  {

    bossSM.AddTransition(AstralWeaverIdle, AstralWeaverAttacks, () => !owner.IsDead && owner.canMove && owner.AttackModule.isAttacking);
    bossSM.AddTransition(AstralWeaverAttacks, AstralWeaverIdle, () => !owner.IsDead && owner.canMove && !owner.AttackModule.isAttacking);

  }
}