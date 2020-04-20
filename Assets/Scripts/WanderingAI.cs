using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
 
public class WanderingAI : MonoBehaviour {
 
    public float wanderRadius;
    public float wanderTimer;
    public ThirdPersonCharacter character;

    private Animator animator;
    private Transform target;
    private NavMeshAgent agent;
    private Rigidbody rigidbody;
    private float timer;
    private Vector3 lastPosition;
 
    // This allows the villagers to wander around the island
    void Start() {
        lastPosition = transform.position;
        agent = GetComponent<NavMeshAgent> ();
        agent.updateRotation = false;
        animator = GetComponent<Animator>();
        timer = wanderTimer;
    }
 
    void Update () {
        timer += Time.deltaTime;
 
        if (timer >= wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
        if (lastPosition != transform.position) {
            character.Move(agent.desiredVelocity, false, false);
        } else {
            character.Move(Vector3.zero, false, false);
        }
    }
    
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}