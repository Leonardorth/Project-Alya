using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroCutsceneManager : MonoBehaviour
{

    [SerializeField] GameObject animationActor;
    [SerializeField] GameObject player;
    [SerializeField] GameObject ui;
    [SerializeField] PlayableDirector timeline;

    void Start()
    {
        player.SetActive(false);
        animationActor.SetActive(true);
        ui.SetActive(false);
        timeline = GetComponent<PlayableDirector>();
        timeline.Play();
    }


    
}
