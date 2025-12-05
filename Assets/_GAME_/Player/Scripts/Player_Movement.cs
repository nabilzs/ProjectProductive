using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player_Movement : MonoBehaviour
{
    // region Enums
    private enum Directions { UP, DOWN, LEFT, RIGHT }
    // endregion

    // region Editor Data
    [Header("Movement Attaributes")]
    [SerializeField] float _moveSpeed = 50f;
    [SerializeField] float _sprintMultiplier = 2f;

    [Header("Dependencies")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;
    // endregion

    // region Internal Data
    private Vector2 _moveDir = Vector2.zero;
    private Directions _facingDirection = Directions.RIGHT;
    private bool _isSprinting = false;

    private readonly int _animMoveRight = Animator.StringToHash("Anim_Player_Move_Right");
    private readonly int _animIdleRight = Animator.StringToHash("Anim_Player_Idle_Right");
    private readonly int _animMoveDown = Animator.StringToHash("Anim_Player_Move_Down");
    private readonly int _animIdleDown = Animator.StringToHash("Anim_Player_Idle_Down");
    private readonly int _animMoveUp = Animator.StringToHash("Anim_Player_Move_Up");
    private readonly int _animIdleUp = Animator.StringToHash("Anim_Player_Idle_Up");
    // endregion

    // region Tick
    private void Update()
    {
        GatherInput();
        CalculateSprint();
        CalculateFacingDirection();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }
    // endregion

    // region Input Logic
    private void GatherInput()
    {
        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");
    }
    // endregion

    // region Movement Logic
    private void MovementUpdate()
    {
        float finalSpeed = _moveSpeed;
        
        if (_isSprinting && _moveDir.sqrMagnitude > 0)
        {
            finalSpeed *= _sprintMultiplier;
        }

        _rb.linearVelocity = _moveDir.normalized * finalSpeed * Time.fixedDeltaTime;
    }
    // endregion

    // region Sprint Logic
    private void CalculateSprint()
    {
        _isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
    // endregion

    // region Animation Logic
    private void CalculateFacingDirection()
    {
        if (_moveDir.x != 0)
        {
            if (_moveDir.x > 0)
            {
                _facingDirection = Directions.RIGHT;
            }
            else if (_moveDir.x < 0)
            {
                _facingDirection = Directions.LEFT;
            }
        }

        if (_moveDir.y != 0)
        {
            if (_moveDir.y > 0)
            {
                _facingDirection = Directions.UP;
            }
            else if (_moveDir.y < 0)
            {
                _facingDirection = Directions.DOWN;
            }
        }
    }

    private void UpdateAnimation()
    {
        if (_facingDirection == Directions.LEFT)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_facingDirection == Directions.RIGHT)
        {
            _spriteRenderer.flipX = false;
        }

        var isMoving = _moveDir.SqrMagnitude() > 0;
        var animation = _animMoveRight;
        switch (_facingDirection )
        {
            case Directions.UP:
                animation = isMoving ? _animMoveUp : _animIdleUp;
                break;
            case Directions.DOWN:
                animation = isMoving ? _animMoveDown : _animIdleDown;
                break;
            case Directions.LEFT:
            case Directions.RIGHT:
            default:
                animation = isMoving ? _animMoveRight : _animIdleRight;
                break;
        }
        _animator.CrossFade(animation, 0);
    }
    // endregion
}