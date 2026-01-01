using System.Collections;
using UnityEngine;
using System;

public class PlayerMovementState : PlayerMovement
{
    //public enum MoveState
    //{
    //    Idle,

    //    Run,

    //    Jump,

    //    Fall,

    //    //Double_Jump,

    //    //Wall_Jump,
    //}

    //public MoveState CurrentMoveState { get; private set; }

    //[SerializeField] private Animator animator;

    //[SerializeField] private Rigidbody2D rigidBody;

    //private const string idleAnim = "Idle";

    //private const string runAnim = "Run";

    //private const string jumpAnim = "Jump";

    //private const string fallAnim = "Fall";

    ////private const string doubleJumpAnim = "Double Jump";

    ////private const string wallJumpAnim = "Wall Jump";

    //public static Action<MoveState> OnplayerMoveStateChanged;

    //private float xPosLastFrame;

    //private void Start()
    //{
    //    xPosLastFrame = transform.position.x;
    //}

    //private void Update()
    //{
    //    float currentXPos = transform.position.x;
    //    float horizontalVelocity = rigidBody.linearVelocity.x;
    //    float verticalVelocity = rigidBody.linearVelocity.y;

    //    // Use a small threshold for position comparison to handle floating point precision
    //    float positionThreshold = 0.001f;
    //    bool isMovingHorizontally = Mathf.Abs(currentXPos - xPosLastFrame) > positionThreshold || Mathf.Abs(horizontalVelocity) > 0.1f;

    //    if (!isMovingHorizontally && verticalVelocity == 0)
    //    {
    //        SetMoveState(MoveState.Idle);
    //    }
    //    else if (isMovingHorizontally && verticalVelocity == 0)
    //    {
    //        SetMoveState(MoveState.Run);
    //    }
    //    else if (verticalVelocity < 0)
    //    {
    //        SetMoveState(MoveState.Fall);
    //    }

    //    // Update xPosLastFrame at the end of the frame
    //    xPosLastFrame = currentXPos;
    //}

    //public void SetMoveState(MoveState moveState)
    //{
    //    if (moveState == CurrentMoveState) return;

    //    switch (moveState)
    //    {
    //        case MoveState.Idle:
    //            HandleIdle();
    //            break;

    //        case MoveState.Run:
    //            HandleRun();
    //            break;

    //        case MoveState.Jump:
    //            HandleJump();
    //            break;

    //        case MoveState.Fall:
    //            HandleFall();
    //            break;

    //        //case MoveState.Double_Jump:
    //        //    HandleDoubleJump();
    //        //    break;

    //        //case MoveState.Wall_Jump:
    //        //    HandleWallJump();
    //        //    break;

    //        default:
    //            Debug.LogError($"Invalid movement state: {moveState}");
    //            break;
    //    }

    //    OnplayerMoveStateChanged?.Invoke(moveState);
    //    CurrentMoveState = moveState;
    //}

    //private void HandleIdle()
    //{
    //    animator.Play(idleAnim);
    //}

    //private void HandleRun()
    //{
    //    animator.Play(runAnim);
    //}

    //private void HandleJump()
    //{
    //    animator.Play(jumpAnim);
    //}

    //private void HandleFall()
    //{
    //    animator.Play(fallAnim);
    //}

    ////private void HandleDoubleJump()
    ////{
    ////    animator.Play(doubleJumpAnim);
    ////}

    ////private void HandleWallJump()
    ////{
    ////    animator.Play(wallJumpAnim);
    ////}

}
