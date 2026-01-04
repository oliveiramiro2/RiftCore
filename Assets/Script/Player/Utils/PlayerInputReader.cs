// using UnityEngine;
// using UnityEngine.InputSystem;

// [DisallowMultipleComponent]
// public class PlayerInputReader : MonoBehaviour
// {
//   public Vector2 MoveInput { get; private set; }

//   // one-frame flags
//   public bool JumpPressed { get; private set; }
//   public bool JumpHeld { get; private set; }
//   public bool AttackPressed { get; private set; }
//   public bool DashPressed { get; private set; }
//   public bool Start { get; private set; }

//   // buffer 
//   public bool AttackBuffered { get; private set; }
//   private float bufferTimer = 0f;
//   public float bufferDuration = 0.15f;

//   // references
//   public InputActionReference confirmAction;

//   void Update()
//   {
//     Read();
//     UpdateBuffer();
//   }

//   public void Read()
//   {
//     MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

//     JumpPressed = Input.GetButtonDown("Jump");
//     JumpHeld = Input.GetButton("Jump");
//     AttackPressed = Input.GetButtonDown("Fire1");
//     DashPressed = Input.GetButtonDown("Fire3");

//     if (confirmAction.action.WasPressedThisFrame())
//     {
//       Start = confirmAction.action.ReadValue<float>() > 0f;
//     }

//     if (AttackPressed)
//     {
//       AttackBuffered = true;
//       bufferTimer = bufferDuration;
//     }
//   }

//   private void UpdateBuffer()
//   {
//     if (AttackBuffered)
//     {
//       bufferTimer -= Time.deltaTime;
//       if (bufferTimer <= 0f)
//       {
//         AttackBuffered = false;
//         bufferTimer = 0f;
//       }
//     }
//   }

//   // consume o buffer attack
//   public void ConsumeAttackBuffer()
//   {
//     AttackBuffered = false;
//     bufferTimer = 0f;
//   }

//   // clear one-frame inputs
//   public void ResetOneFrameInputs()
//   {
//     JumpPressed = false;
//     AttackPressed = false;
//     DashPressed = false;
//   }

//   void OnEnable()
//   {
//     confirmAction.action.Enable();
//   }

//   void OnDisable()
//   {
//     confirmAction.action.Disable();
//   }
// }


using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerInputReader : MonoBehaviour
{
  private PlayerController player;

  public Vector2 MoveInput { get; private set; }
  public bool JumpPressed { get; private set; }
  public bool JumpHeld { get; private set; }
  public bool AttackPressed { get; private set; }
  public bool DashPressed { get; private set; }
  public bool ConfirmPressed { get; private set; }
  public bool AttackBuffered { get; private set; }
  private float bufferTimer;

  [Header("Buffer Settings")]
  [SerializeField] private float bufferDuration = 0.15f;
  private bool isPaused = false;

  public GameObject pauseMenu;


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
    if (!context.performed) return;

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
      isPaused = true;
    }
  }

  void Update()
  {

    UpdateAttackBuffer();

    if (isPaused)
    {
      player.canMove = false;
      Time.timeScale = 0f;
      isPaused = false;
      EnterPauseMenu();
    }
  }

  private void UpdateAttackBuffer()
  {
    if (!AttackBuffered) return;

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
  }
}
