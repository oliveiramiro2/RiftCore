using UnityEngine;

public class AstralWeaverController : BaseEntity
{
    [Header("Boss runtime")]
    public float MoveSpeed = 2f;
    public bool IsFacingRight() => transform.localScale.x > 0;
    public bool Phase2() => currentHealth <= (maxHealth * 0.5f);

    public AstralWeaverStateMachine AstralWeaverSM { get; private set; }
    public AstralWeaverStateFactory AstralWeaverStateFactory { get; private set; }


    public bool CanMove = true;

    public void SetupModules(AstralWeaverStateMachine sm, AstralWeaverStateFactory factory)
    {
        AstralWeaverSM = sm;
        AstralWeaverStateFactory = factory;
    }

    public void FlipX(bool faceRight)
    {
        if (spriteRenderer != null) transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
    }

}
