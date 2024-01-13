using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] int enemyHealth = 200;

    //NavMesh
    public NavMeshAgent enemyAgent;
    public Transform player;
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    //Patrolling
    public Vector3 walkPoint;
    public float walkPointRange;
    public bool walkPointSet;

    //
    public float sightRange, attackRange;
    public bool enemySightRange, enemyAttackRange;

    //attack
    public float attackDelay;
    public bool isAttacking;
    public Transform attackPoint;
    public GameObject projectile;
    public float projectileForce = 18f;
    public Animator enemyAnimator;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        enemyAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        enemySightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        enemyAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!enemySightRange && !enemyAttackRange)
        {
            Patrolling();
            enemyAnimator.SetBool("Patrolling", true);
            enemyAnimator.SetBool("PlayerDetected", false);
            enemyAnimator.SetBool("PlayerAttacking", false);
        }
        else if (enemySightRange && !enemyAttackRange)
        {
            DetectPlayer();
            enemyAnimator.SetBool("Patrolling", false);
            enemyAnimator.SetBool("PlayerDetected", true );
            enemyAnimator.SetBool("PlayerAttacking", false );
        }
        else if (enemySightRange && enemyAttackRange)
        {
            AttackToPlayer();
            enemyAnimator.SetBool("Patrolling", false);
            enemyAnimator.SetBool("PlayerDetected", false);
            enemyAnimator.SetBool("PlayerAttacking", true );
        }
    }

    void Patrolling()
    {
        if (walkPointSet == false)
        {
            float randomZPos = Random.Range(-walkPointRange, walkPointRange);
            float randomxPos = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomxPos,
                                    transform.position.y, transform.position.z + randomZPos);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
            {
                walkPointSet = true;
            }
        }

        if (walkPointSet == true)
        {
            enemyAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1)
        {
            walkPointSet = false;
        }
    }


    void DetectPlayer()
    {
        enemyAgent.SetDestination(player.position);
        transform.LookAt(player);
    }

    void AttackToPlayer()
    {
        enemyAgent.SetDestination(transform.position);
        transform.LookAt(player);

        if (isAttacking == false)
        {
            //Atak türü
            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileForce, ForceMode.Impulse);
            isAttacking = true;
            Invoke("ResetAttack", attackDelay);

        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    public void EnemyTakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;

        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
