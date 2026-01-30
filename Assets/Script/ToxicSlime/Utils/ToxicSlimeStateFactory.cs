using UnityEngine;

public class ToxicSlimeStateFactory : MonoBehaviour
{
  [HideInInspector] public ToxicSlimeStateMachine bossSM;
  [HideInInspector] public ToxicSlimeController owner;

  public bool isIdle = true;


  public ToxicSlimeIdle ToxicSlimeIdle { get; private set; }
  public ToxicSlimeAttack ToxicSlimeAttack { get; private set; }

  void Awake()
  {
    ToxicSlimeIdle = new ToxicSlimeIdle();
    ToxicSlimeAttack = new ToxicSlimeAttack();
  }

  public void InitializeTransitions()
  {

    bossSM.AddTransition(ToxicSlimeIdle, ToxicSlimeAttack, () => owner.isAttacking);
    bossSM.AddTransition(ToxicSlimeAttack, ToxicSlimeIdle, () => !owner.isAttacking && !owner.canRoll);

  }
}