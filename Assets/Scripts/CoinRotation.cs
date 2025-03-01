using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 20f;
    void Update()
    {
        transform.Rotate(0,rotationSpeed*Time.deltaTime,0);
    }
}
