using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 60f;
    void Update()
    {
        transform.Rotate(0,rotationSpeed*Time.deltaTime,0);
    }
    
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
            scoreManager.IncrementScore();
        }
        Destroy(gameObject);
    }

}
