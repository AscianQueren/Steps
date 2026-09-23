using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Grounded,
    Hopping,
    Airborne,
    Stunned
}

public class Player : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private float slideSpeed = 2f;    // lanes per second (lane goes -1..1)
    [SerializeField] private float laneHalfWidth = 3f; // temporary until StairPerspective places the player

    private PlayerInput playerInput;
    private InputAction slideAction;
    private InputAction stepUp1Action;
    private InputAction stepUp2Action;
    private InputAction hopAction;

    private PlayerState state = PlayerState.Grounded;
    private int stepIndex;
    private float lane;

    private void Awake()
    {
    }

    // Actions are cached in Start: PlayerInput gives each player its own copy of the actions during its own setup.
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        slideAction = playerInput.actions["Slide"];
        stepUp1Action = playerInput.actions["StepUp1"];
        stepUp2Action = playerInput.actions["StepUp2"];
        hopAction = playerInput.actions["Hop"];
    }

    public void Setup(Color playerColor, float startLane)
    {
        color = playerColor;
        lane = startLane;
        GetComponent<SpriteRenderer>().color = playerColor;
    }

    private void Update()
    {
        ReadInput();
        UpdateVisuals();
    }

    private void ReadInput()
    {
        if (playerInput == null)
        {
            return;
        }

        Slide(slideAction.ReadValue<float>());

        if (stepUp1Action.WasPressedThisFrame())
        {
            RequestMove(MoveIntent.StepUp1);
        }

        if (stepUp2Action.WasPressedThisFrame())
        {
            RequestMove(MoveIntent.StepUp2);
        }

        if (hopAction.WasPressedThisFrame())
        {
            RequestMove(MoveIntent.HopInPlace);
        }
    }

    public void OnBeat(int beat)
    {
    }

    // TODO: wait for the beat (BeatClock) and ask MoveRules before moving.
    public void RequestMove(MoveIntent intent)
    {
        switch (intent)
        {
            case MoveIntent.StepUp1:
                StepUp(1);
                break;
            case MoveIntent.StepUp2:
                StepUp(2);
                break;
            case MoveIntent.HopInPlace:
                HopInPlace();
                break;
        }
    }

    private void StepUp(int count)
    {
        stepIndex += count;
        Debug.Log($"Player {playerInput.playerIndex + 1} stepped up {count} -> step {stepIndex}");
    }

    private void HopInPlace()
    {
        Debug.Log($"Player {playerInput.playerIndex + 1} hopped in place on step {stepIndex}");
    }

    private void Land()
    {
    }

    private void Slide(float input)
    {
        lane = Mathf.Clamp(lane + input * slideSpeed * Time.deltaTime, -1f, 1f);
    }

    public bool IsBlockedBy(Player other)
    {
        return default;
    }

    public int GetStepIndex()
    {
        return stepIndex;
    }

    public float GetLane()
    {
        return lane;
    }

    public Color GetColor()
    {
        return color;
    }

    public bool IsGrounded()
    {
        return state == PlayerState.Grounded;
    }

    public void Hit()
    {
    }

    private void UpdateVisuals()
    {
        Vector3 position = transform.localPosition;
        position.x = lane * laneHalfWidth;
        transform.localPosition = position;
    }
}
