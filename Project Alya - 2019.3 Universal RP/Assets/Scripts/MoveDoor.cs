using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] int speed;
    bool nearDoor;
    bool istriggered;
    private float startTime;
    private Vector3 start;
    private Vector3 des;
    private float journeyLength;

    public AudioClip triggerSound;
    AudioSource audioSource;

    void Start()
    {
        start = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);
        des = new Vector3(door.transform.position.x, -3.55f, door.transform.position.z);
        journeyLength = Vector3.Distance(start, des);
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        if (nearDoor == true)
        {
            door.transform.position = Vector3.Lerp(start, des, fractionOfJourney);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        
        if(istriggered == false)
        {
            startTime = Time.time;
            nearDoor = true;
            audioSource.PlayOneShot(triggerSound, 0.7f);
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        istriggered = true;
    }
}
