using UnityEngine;

public class LasterStateFactory : MonoBehaviour
{
  [HideInInspector] public LasterStateMachine bossSM;
  [HideInInspector] public LasterController owner;

  public bool isIdle = true;


  public LasterIdle LasterIdle { get; private set; }
  public LasterAttack LasterAttack { get; private set; }

  void Awake()
  {
    LasterIdle = new LasterIdle();
    LasterAttack = new LasterAttack();
  }

  public void InitializeTransitions()
  {

    bossSM.AddTransition(LasterIdle, LasterAttack, () => !owner.IsDead && owner.isAttacking);

    bossSM.AddTransition(LasterAttack, LasterIdle, () => !owner.IsDead && !owner.isAttacking);


    //bossSM.AddAnyTransition(LasterDeath, () => owner.IsDead);

  }
}