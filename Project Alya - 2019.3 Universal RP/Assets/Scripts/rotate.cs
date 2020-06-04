using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    [SerializeField] float yAxis = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, yAxis, 0 * Time.deltaTime);
    }
}
