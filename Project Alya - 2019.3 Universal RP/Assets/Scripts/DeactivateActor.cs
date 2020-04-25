using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateActor : MonoBehaviour
{
    [SerializeField] GameObject animationActor;
    

    void Start()
    {
        animationActor.SetActive(false);
    }
}
