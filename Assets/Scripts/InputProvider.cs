using UnityEngine;

public sealed class InputProvider : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MoveButton _leftButton = default;
    [SerializeField] private MoveButton _rightButton = default;
    [SerializeField] private MoveButton _attack = default;
    [SerializeField] private MoveButton _jump = default;

    private bool _attackTriggered = false;
    private bool _jumpTriggered = false;

    private void Update()
    {
        var horizontalAxis = Input.GetAxis("Horizontal");

        // Handle horizontal movement
        if (_leftButton.IsPressing)
        {
            _playerController.MoveLeft();
        }
        else if (_rightButton.IsPressing)
        {
            _playerController.MoveRight();
        }
        else if (horizontalAxis < 0f)
        {
            _playerController.MoveLeft();
        }
        else if (horizontalAxis > 0f)
        {
            _playerController.MoveRight();
        }

        if (!_leftButton.IsPressing && !_rightButton.IsPressing && horizontalAxis == 0)
        {
            _playerController.StopMoving();
        }

        // Handle attack (triggered once per press)
        if (_attack.IsPressing && !_attackTriggered)
        {
            _playerController.Attack();
            _attackTriggered = true;
        }
        else if (!_attack.IsPressing)
        {
            _attackTriggered = false;
        }

        // Handle jump (triggered once per press)
        if (_jump.IsPressing && !_jumpTriggered)
        {
            _playerController.Jump();
            _jumpTriggered = true;
        }
        else if (!_jump.IsPressing)
        {
            _jumpTriggered = false;
        }
    }
}