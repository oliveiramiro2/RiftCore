using UnityEngine;

public class AstralWeaverStateFactory : MonoBehaviour
{
  [HideInInspector] public AstralWeaverStateMachine bossSM;
  [HideInInspector] public AstralWeaverController owner;
  [HideInInspector] public AstralWeaverAnimationBridge animatorBridge;

  public bool isIdle = true;


  public AstralWeaverIdle AstralWeaverIdle { get; private set; }

  void Awake()
  {
    AstralWeaverIdle = new AstralWeaverIdle();
  }

  public void InitializeTransitions()
  {
    
  }
}