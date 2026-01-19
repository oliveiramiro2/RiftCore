using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputReader : MonoBehaviour
{
  private PlayerController player;
  public PlayerInput playerInput;

  public Vector2 MoveInput { get; private set; }
  public bool JumpPressed { get; private set; }
  public bool JumpHeld { get; private set; }
  public bool AttackPressed { get; private set; }
  public bool DashPressed { get; private set; }
  public bool ConfirmPressed { get; private set; }
  public bool AttackBuffered { get; private set; }
  public bool BuffSword { get; private set; }
  public bool CastSpell { get; private set; }
  private float bufferTimer;

  public GameObject firstButton;

  [Header("Buffer Settings")]
  [SerializeField] private float bufferDuration = 0.15f;
  private bool isPaused = false;

  public GameObject pauseMenu;

  void Start()
  {
    playerInput = GetComponent<PlayerInput>();
  }


  public void Initialize(PlayerController owner)
  {
    this.player = owner;
  }

  public void OnMove(InputAction.CallbackContext context)
  {
    MoveInput = context.ReadValue<Vector2>();
  }

  public void OnJump(InputAction.CallbackContext context)
  {
    if (context.started)
      JumpPressed = true;

    JumpHeld = context.performed;
  }

  public void OnAttack(InputAction.CallbackContext context)
  {
    if (!context.performed || !player.canMove) return;

    AttackPressed = true;
    AttackBuffered = true;
    bufferTimer = bufferDuration;
  }

  public void OnDash(InputAction.CallbackContext context)
  {
    if (context.performed)
      DashPressed = true;
  }

  public void OnConfirm(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      player.enabled = false;
      playerInput.actions.FindActionMap("Player").Disable();
      playerInput.actions.FindActionMap("UI").Enable();
      Time.timeScale = 0f;
      isPaused = true;
    }

  }
  public void OnBuff(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      BuffSword = true;
    }
  }

  public void OnSpell(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      CastSpell = true;
    }
  }

  void Update()
  {

    UpdateAttackBuffer();

    if (isPaused)
    {
      player.canMove = false;
      isPaused = false;
      EnterPauseMenu();
    }
  }

  private void UpdateAttackBuffer()
  {
    if (!AttackBuffered || !player.canMove) return;

    bufferTimer -= Time.deltaTime;
    if (bufferTimer <= 0f)
    {
      AttackBuffered = false;
      bufferTimer = 0f;
    }
  }

  public void EnterPauseMenu()
  {
    pauseMenu.SetActive(true);
  }

  public void GoToMainMenu()
  {
    Time.timeScale = 1f;
    UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
  }

  public void ExitPauseMenu()
  {

    player.enabled = true;
    playerInput.actions.FindActionMap("UI").Disable();
    playerInput.actions.FindActionMap("Player").Enable();
    Time.timeScale = 1f;
    pauseMenu.SetActive(false);
    player.canMove = true;
  }

  public void ConsumeAttackBuffer()
  {
    AttackBuffered = false;
    bufferTimer = 0f;
  }

  public void ResetOneFrameInputs()
  {
    JumpPressed = false;
    AttackPressed = false;
    DashPressed = false;
    ConfirmPressed = false;
    CastSpell = false;
    BuffSword = false;
  }
}
