using UnityEngine;

public class MoveVcam4Focus : MonoBehaviour
{
    [SerializeField] int speed;
    
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
    
}
