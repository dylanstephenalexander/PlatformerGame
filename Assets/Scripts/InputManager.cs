using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent <Vector3> OnMove = new UnityEvent<Vector3>();
    public UnityEvent OnSpacePressed = new UnityEvent();
    public UnityEvent OnSpaceReleased = new UnityEvent();
    public UnityEvent OnResetPressed = new UnityEvent();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            OnSpacePressed?.Invoke();
            Debug.Log("space pressed");
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            OnSpaceReleased?.Invoke();
        }
        Vector3 input = Vector3.zero;
        if(Input.GetKey(KeyCode.A)){
            input += Vector3.left;
        }
        if(Input.GetKey(KeyCode.D)){
            input += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            input += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input += Vector3.back;
        }
        OnMove?.Invoke(input);
        
        if(Input.GetKeyDown(KeyCode.R)){
            OnResetPressed?.Invoke();
        }
    }
}
