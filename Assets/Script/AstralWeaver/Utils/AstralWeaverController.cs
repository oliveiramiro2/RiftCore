using UnityEngine;

public class AstralWeaverController : BaseEntity
{
    [Header("Boss runtime")]
    public float MoveSpeed = 2f;
    public bool IsFacingRight() => transform.localScale.x > 0;
    public bool Phase2() => currentHealth <= (maxHealth * 0.5f);

    public bool CanMove = true;



    public void FlipX(bool faceRight)
    {
        if (spriteRenderer != null) transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
    }

}
