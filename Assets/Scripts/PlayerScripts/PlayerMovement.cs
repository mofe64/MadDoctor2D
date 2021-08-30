using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.5f;
    [SerializeField]
    private float minBound_x = -71f, maxBound_x = 71f, minBound_y = -3.3f, maxBound_y = 0f;

    private Vector3 tempPosition;
    private float xAxis, yAxis;

    private PlayerAnimations playerAnimation;

    [SerializeField]
    private float shootWaitTime = 0.5f;
    private float waitBeforeShooting;

    [SerializeField]
    private float moveWaitTime = 0.3f; //time duration to wait before allowing the player move after shooting

    private float waitBeforeMoving;

    private bool canMove = true;
    private PlayerShootingManager shootingManager;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimations>();
        shootingManager = GetComponent<PlayerShootingManager>();
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAnimation();
        HandlePlayerDirection();
        HandleShooting();
        CheckIfCanMove();
    }

    void HandleMovement()
    {
        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        if (!canMove) { return; } //prevent moving while player is shooting

        tempPosition = transform.position;
        tempPosition.x += xAxis * moveSpeed * Time.deltaTime;
        tempPosition.y += yAxis * moveSpeed * Time.deltaTime;
        if (tempPosition.x < minBound_x) { tempPosition.x = minBound_x; }
        if (tempPosition.x > maxBound_x) { tempPosition.x = maxBound_x; }
        if (tempPosition.y < minBound_y) { tempPosition.y = minBound_y; }
        if (tempPosition.y > maxBound_y) { tempPosition.y = maxBound_y; }
        transform.position = tempPosition;
    }

    void HandleAnimation()
    {
        if (!canMove) { return; } // prevent movement animation while player is shooting
        //by using abs we convert both positive movement and negative movement to a positive value and compare with 0
        if (Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
        {
            playerAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
        }
        else
        {
            playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        }
    }

    void HandlePlayerDirection()
    {
        if (xAxis > 0)
        {
            playerAnimation.setFacingDirection(true);
        }
        else if (xAxis < 0)
        {
            playerAnimation.setFacingDirection(false);
        }
    }

    void StopMovement()
    {
        canMove = false;
        waitBeforeMoving = Time.time + moveWaitTime;
        //Time.time is measured time in game, ie time since game started
    }

    void CheckIfCanMove()
    {
        canMove = Time.time > waitBeforeMoving;
    }

    void Shoot()
    {
        waitBeforeShooting = Time.time + shootWaitTime; //restrict players shooting
        StopMovement();
        playerAnimation.PlayAnimation(TagManager.SHOOT_ANIMATION_NAME);
        shootingManager.Shoot(transform.localScale.x);
    }
    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > waitBeforeShooting)
            {
                Shoot();
            }
        }
    }
}
