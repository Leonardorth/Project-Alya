using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] GameObject player;
    [SerializeField] GameObject elevatorBorderColliders;
    bool onElevator;
    bool istriggered;
    private float startTime;
    private Vector3 start;
    private Vector3 des;
    private float journeyLength;
    public float elevatorDirection; //positive numbers for going up, negative for going down

    public AudioClip triggerSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        des = new Vector3(transform.position.x, elevatorDirection, transform.position.z);
        journeyLength = Vector3.Distance(start, des);
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        if (onElevator == true)
        {
            transform.position = Vector3.Lerp(start, des, fractionOfJourney);
            
            elevatorBorderColliders.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (istriggered == false)
        {
            startTime = Time.time;
            onElevator = true;
            player.transform.parent = transform;
            audioSource.PlayOneShot(triggerSound, 0.8f);
            
            
        }

    }

    void OnTriggerExit(Collider other)
    {
        istriggered = true;
    }
}
