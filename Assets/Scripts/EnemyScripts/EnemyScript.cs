using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(!agent.isOnNavMesh)
        {
            agent.enabled = false;
            agent.enabled = true;
        }

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); //Player, field of view, Layer mask?
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);//Player, Attack range, Layer mask?

        if (!playerInSightRange && !playerInAttackRange) Patroling(); //not in sight not in range patrol
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();//in sight but not in range chase
        if (playerInAttackRange && playerInSightRange) AttackPlayer();//in sight and in range attack
        Debug.Log($"Sight: {playerInSightRange}, Attack: {playerInAttackRange}");
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint(); //no walk point? search for it

        if (walkPointSet) //if walk point set
            agent.SetDestination(walkPoint);//make the agent(the Enemy) go to a destination, in this case the walkPoint

        Vector3 distanceToWalkPoint = transform.position - walkPoint; //not sure how this works

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false; //now restart the whole progress and search for a new walkPoint
    }

    void EnableAgent()
    {

    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange); //random point between negative and positive in x
        float randomX = Random.Range(-walkPointRange, walkPointRange);//same for y

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);//The position ai wants to walk towards. It ranges from your RandomX and RandomZ and Y states the same so it doesn't randomly jump

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) //Makes sure the walkpoint is on the ground
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position); //The agents heads towards the destination (player)
    }

    private void AttackPlayer()
    {

        agent.SetDestination(transform.position); // it stops ai from moving while attacking once in range

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Debug.Log($"Attacked {player.name} on frame {Time.frameCount}");
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); //calls ResetAttack everyone few seconds
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
