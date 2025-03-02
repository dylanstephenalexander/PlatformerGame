using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = 20f;
    private CharacterController charController;
    [SerializeField] private bool touchingGround;
    [SerializeField] private bool canDoubleJump;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private bool canDash;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashSpeed = 30f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCoolDown = 1f;
    void Start()
    {
        charController = GetComponent<CharacterController>();
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnSpacePressed.AddListener(Jump);
        inputManager.OnDashPressed.AddListener(PerformDash);
        canDash = true;
    }
    void Update(){
        if(isDashing){
            charController.Move(velocity * Time.deltaTime);
            return;
        }
        touchingGround = charController.isGrounded;
        if(!touchingGround){
            velocity.y -= gravity *Time.deltaTime;
        }
        Vector3 calculatedMove = moveDirection*movementSpeed + new Vector3(0,velocity.y,0);
        charController.Move(calculatedMove*Time.deltaTime);
    }
    private void MovePlayer(Vector3 direction){
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();
        moveDirection = (cameraForward * direction.z + cameraRight * direction.x).normalized;
    }
    private void Jump(){
        if(touchingGround){
            velocity.y = jumpForce; 
            canDoubleJump = true;
        }
        else if(canDoubleJump){
            velocity.y += jumpForce * 0.5f;
            canDoubleJump = false;
        }
    }
    private void PerformDash(){
        if(!canDash){
            return;
        }
        else {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        gravity = 0f; //suspend player in air
        velocity = moveDirection * dashSpeed; //enhance velocity while dashing
        yield return new WaitForSeconds(dashTime); //timer for dash. While dashing, the enhanced velocity is used to move the player in the update() method
        gravity = 20f; //end dash and readjust parameters for normal movement
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
}
