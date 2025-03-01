using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = 20f;
    private CharacterController charController;
    [SerializeField] private bool touchingGround;
    [SerializeField] private bool canDoubleJump;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 moveDirection;
    void Start()
    {
        charController = GetComponent<CharacterController>();
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnSpacePressed.AddListener(Jump);
    }
    void Update(){
        touchingGround = charController.isGrounded;
        if(!touchingGround){
            velocity.y -= gravity *Time.deltaTime;
        }
        Vector3 calculatedMove = moveDirection*movementSpeed + new Vector3(0,velocity.y,0);
        charController.Move(calculatedMove*Time.deltaTime);
    }
    private void MovePlayer(Vector3 direction){
        Debug.Log("move player called " + transform.position);
        moveDirection = new(direction.x, 0, direction.z);
    }
    private void Jump(){
        Debug.Log("jump method called");
        if(touchingGround){
            velocity.y = jumpForce; 
            canDoubleJump = true;
        }
        else if(canDoubleJump){
            velocity.y += jumpForce * 0.5f;
            canDoubleJump = false;
        }

    }
}
