using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;



public class Vihu_1 : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public bool isAlive;

    public Vector3 walkpoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackrange;
    public GameObject Blood;

    RaycastHit hit;

    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
    }

    
    private void Update()
    {


        if (isAlive == true)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackrange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (!playerInSightRange && !playerInAttackrange) Patrolling();
            if (playerInSightRange && !playerInAttackrange) transform.LookAt(player); ChasePlayer();
            NavMeshHit hit;

            if (playerInSightRange && playerInAttackrange)
            {
                if (!agent.Raycast(player.position, out hit))
                {
                    AttackPlayer();

                    // Target is "visible" from our position.
                }
            }
            
           
             




        }
        

        
        

        
        

        
    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkpoint);

        Vector3 distanceToWalkPoint = transform.position - walkpoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ  = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkpoint, -transform.up, 0f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 100f, ForceMode.Impulse);
            rb.AddForce(transform.up * 1f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        Instantiate(Blood, transform.position, Quaternion.identity);
        health -= damage;


        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        isAlive = false;
        

        
        
        
        
        
        
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("PlayerCapsule"))
        {
            TakeDamage(50);
        }
    }

    //Made by Arttu Nykänen

}
