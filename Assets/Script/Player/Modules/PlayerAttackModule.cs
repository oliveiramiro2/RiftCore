using UnityEngine;

[DisallowMultipleComponent]

[RequireComponent(typeof(PlayerController))]
public class PlayerAttackModule : MonoBehaviour
{

  private PlayerController player;

  [Header("Combo Settings")]
  public int maxCombo = 3;
  public float finishCooldown = 1f;
  public bool resetComboOnFinish = false;

  private int comboStep = 0;
  private bool comboWindowOpen = false;

  private bool onCooldown = false;
  private float cooldownTimer = 0f;

  public int CurrentStep => comboStep;
  public bool IsOnCooldown() => onCooldown;
  public bool IsInComboWindow() => comboWindowOpen;



  public void Initialize(PlayerController owner)
  {
    this.player = owner;
  }

  void Update()
  {
    if (onCooldown)
    {
      cooldownTimer -= Time.deltaTime;
      if (cooldownTimer <= 0f)
      {
        onCooldown = false;
        cooldownTimer = 0f;
      }
    }
  }

  public void OpenComboWindow()
  {
    comboWindowOpen = true;
    PlayAttackSFX();
  }

  public void PlayAttackSFX()
  {
    player.events.OnAttack.Raise();
  }

  public void CloseComboWindow()
  {
    comboWindowOpen = false;
  }

  public int StartOrAdvanceCombo()
  {
    if (onCooldown)
      return comboStep;

    if (comboWindowOpen)
    {
      comboStep++;
      if (comboStep > maxCombo)
      {
        resetComboOnFinish = true;
        comboStep = 1;
      }
    }
    else
    {
      comboStep = 1;
    }

    return comboStep;
  }

  public void ResetCombo()
  {
    comboStep = 0;
    comboWindowOpen = false;
  }

  public void StartCooldown()
  {
    onCooldown = true;
    cooldownTimer = resetComboOnFinish ? 0f : finishCooldown;
    resetComboOnFinish = false;
  }
}
