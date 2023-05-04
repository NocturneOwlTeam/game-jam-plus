using Nocturne.Enums;
using UnityEngine;

namespace Physical
{
    public class PlayerMovement : MonoBehaviour
    {
        /*
        TODO:
        - Add Dash/Slide
        - Add Walljump.
        - Add combat system (if needed)
        - Add crouching
        - Add stomping (mid-air, press crouch and then the character will go down quickly).
        - Refactor it to use states instead.

        References:
        - Rayman Legends
        - Super Mario
        - Guacamelee
        - Celeste
        - Pizza Tower (that one is getting popular nowadays)

        Special thanks to Dawnosaur for the code and guidelines used for this template.
         */
        private Rigidbody2D body;

        [Header("Player Stats")]
        public PlayerMoveStats stats;

        [Header("Animation")]
        [SerializeField]
        private Animator playerAnimator;
        //General Movement

        public bool isPaused { get; set; }

        public PlayerStatus currentStatus { get; private set; }

        private Transform currentTransform;

        private float horizontal;

        //Jumping
        public JumpState currentJumpStatus { get; private set; }

        //private bool jumpInputReleased;

        private bool canDoubleJump;

        private bool isJumping;

        private float lastGroundedTime;

        private float doubleJumpDelay;

        private float lastJumpTime;

        private bool isFacingRight;

        //Ground Checking

        [Header("Ground Checker")]
        public Transform groundChecker;

        public Vector2 lengthChecker;

        public LayerMask groundMasks;

        private void Start()
        {
            body = GetComponent<Rigidbody2D>();
            SetGravity(stats.gravityScale);
            currentTransform = transform;
            if (!playerAnimator)
            {
                playerAnimator = GetComponentInChildren<Animator>();
            }
            isFacingRight = true;
        }

        private void Update()
        {
            SetInput();

            if (Input.GetButtonDown("Jump"))
            {
                OnJump();

                if (CanDoubleJump())
                {
                    Jump();
                }
            }

            if (Input.GetButtonUp("Jump") && CanJumpCut())
            {
                OnJumpCut();
            }

            if (horizontal != 0)
            {
                CheckCurrentDirection(horizontal > 0);
            }
        }

        private void FixedUpdate()
        {
            if (!isPaused)
            {
                MovementSystem();
            }
            JumpPhysics();
        }

        private void SetInput()
        {
            horizontal = Input.GetAxis("Horizontal");
            playerAnimator.SetFloat("Horizontal", Mathf.Abs(horizontal));
        }

        #region Jump

        private void JumpPhysics()
        {
            isJumping = InMidAir();
            playerAnimator.SetBool("MidAir", isJumping);
            if (Physics2D.OverlapBox(groundChecker.position, lengthChecker, 0, groundMasks))
            {
                lastGroundedTime = stats.coyoteTime;
                currentJumpStatus = JumpState.None;
                if (stats.canDoubleJump)
                {
                    canDoubleJump = true;
                    doubleJumpDelay = stats.doubleJumpDelay;
                }
            }
            else
            {
                if (lastGroundedTime > 0)
                {
                    lastGroundedTime -= Time.deltaTime;
                }

                if (lastGroundedTime <= 0 && body.velocity.y < 0)
                {
                    currentJumpStatus = JumpState.Falling;
                }

                if (doubleJumpDelay > 0)
                {
                    doubleJumpDelay -= Time.deltaTime;
                }
            }

            if (lastJumpTime > 0)
            {
                lastJumpTime -= Time.deltaTime;
            }

            if (CanJump() && lastJumpTime > 0)
            {
                Jump();
            }

            if (body.velocity.y < 0)
            {
                SetGravity(stats.gravityScale * stats.customGravityMultiplier);
                //Sets the maximum fall speed. Useful if we are going to include free fall sections.
                body.velocity = new Vector2(body.velocity.x, Mathf.Max(body.velocity.y, - stats.maxFallSpeed));
            }
            else
            {
                SetGravity(stats.gravityScale);
            }
        }

        private void Jump()
        {
            if (CanDoubleJump())
            {
                canDoubleJump = false;
            }

            if (canDoubleJump)
            {
                doubleJumpDelay = stats.doubleJumpDelay;
            }

            lastGroundedTime = 0;
            lastJumpTime = 0;
            //jumpInputReleased = false;

            float force = stats.jumpForce;

            //This neutralizes any gravity force that is happening in the body in the moment.
            //The reason it's negative is because... well, it's gravity, it's a negative force.
            if (body.velocity.y < 0)
                force -= body.velocity.y;

            currentJumpStatus = JumpState.Jumping;

            body.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }

        private bool InMidAir()
        {
            return currentJumpStatus == JumpState.Jumping || currentJumpStatus == JumpState.Falling;
        }

        private bool CanJump()
        {
            return lastGroundedTime > 0 && currentJumpStatus == JumpState.None;
        }

        private bool CanDoubleJump()
        {
            return doubleJumpDelay <= 0 && stats.canDoubleJump && canDoubleJump && InMidAir();
        }

        private bool CanJumpCut()
        {
            return currentJumpStatus == JumpState.Jumping && body.velocity.y > 0;
        }

        private void OnJump()
        {
            lastJumpTime = stats.jumpBufferTime;
        }

        private void OnJumpCut()
        {
            if (body.velocity.y > 0 && currentJumpStatus == JumpState.Jumping)
            {
                body.AddForce(Vector2.down * body.velocity.y * (1 - stats.jumpCutMultiplier), ForceMode2D.Impulse);
            }

            //jumpInputReleased = true;
            lastJumpTime = 0;
        }

        public void SetGravity(float gravity)
        {
            body.gravityScale = gravity;
        }

        #endregion Jump

        #region Movement

        private bool CanSprint()
        {
            return Mathf.Abs(horizontal) > 0 && Input.GetButton("Fire3");
        }

        private void MovementSystem()
        {
            float currentSpeed = CanSprint() ? stats.sprintSpeed : stats.speed;
            float topSpeed = currentSpeed * horizontal;

            //TODO: add a lerp variable
            topSpeed = Mathf.Lerp(body.velocity.x, topSpeed, 1f);

            //Abs is to get the absolute of a number, without the negative.
            float acceleration = (Mathf.Abs(topSpeed) > 0.01f) ? stats.accelerationSpeed : stats.deaccelerationSpeed;

            float speedDif = topSpeed - body.velocity.x;

            float movement = speedDif * acceleration;

            body.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }

        #endregion Movement

        #region Turning

        private void Turn()
        {
            Vector3 scale = currentTransform.localScale;
            scale.x *= -1;
            currentTransform.localScale = scale;
            isFacingRight = !isFacingRight;
        }

        private void CheckCurrentDirection(bool direct)
        {
            if (direct != isFacingRight)
            {
                Turn();
            }
        }

        #endregion Turning
    }
}