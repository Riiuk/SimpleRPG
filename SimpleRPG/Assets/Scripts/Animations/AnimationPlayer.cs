using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AnimationPlayer : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = .1f;

    Animator anim;          // Variable que almacena el componente Animator del player
    NavMeshAgent agent;     // Variable que almacena el componente NavMeshAgent;

	// Use this for initialization
	void Awake ()
    {   
        // Buscamos el componente animator en el hijo del Player
        anim = GetComponentInChildren<Animator>();
        // Buscamos el componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, locomotionAnimationSmoothTime, Time.deltaTime);
	}
}
