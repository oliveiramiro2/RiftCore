using UnityEngine;

public static class AnimatorExtensions
{
    /// <summary>
    /// Retorna true quando a animação atual completou o ciclo (normalizedTime >= 1f).
    /// </summary>
    public static bool IsCurrentAnimationFinished(this Animator animator, int layer = 0)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);

        return info.normalizedTime >= 0.9f;
    }
}
