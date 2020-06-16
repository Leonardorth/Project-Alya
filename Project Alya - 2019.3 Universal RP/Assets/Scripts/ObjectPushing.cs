using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPushing : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] Vector3 offset;
    [SerializeField] float raycastFrequency;
    public float force;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("CheckForPushableObject");
    }
    

    IEnumerator CheckForPushableObject()
    {
        while (true)
        {
            
            if (Physics.Raycast(transform.position + offset, transform.forward, out hit, 0.5f))
            {
                //Debug.Log(hit.collider.name);
                Debug.DrawRay(transform.position + offset, transform.forward, Color.red, 0.5f);
                if (hit.collider.CompareTag("PushableObject"))
                {
                    ApplyForce();
                    animator.SetBool("isPushing", true);
                }
                
            }
            else 
                if (animator.GetBool("isPushing"))
                {
                    animator.SetBool("isPushing", false);
                }

            yield return new WaitForSeconds(raycastFrequency);

        }
    }

    void ApplyForce()
    {
        if (animator.GetBool("isPushing"))
        {

            var targetRigidbody = hit.collider.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                Vector3 direction = hit.transform.position - transform.position;
                targetRigidbody.AddForce(direction.normalized * force);
            }

        }
        
    }
}
