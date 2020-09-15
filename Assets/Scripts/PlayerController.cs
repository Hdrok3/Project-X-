using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // TODO:
    // Burasi değiştirilecek, weapon içerisine koyulacak, weapon attack.
    [SerializeField] private WeaponAttack demoAttack;
    public float threshold = 2f;

    private GameObject _attackTarget;

    NavMeshAgent agent;
    Animator anim;
    Rigidbody rb;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }
    public void MoveToTarget(Vector3 point)
    {
        if (Vector3.Distance(point, transform.position) < threshold)
        {
            return;
        }

        StopAllCoroutines();
        agent.isStopped = false;
        agent.SetDestination(point);
    }


    public void AttackTarget(GameObject target)
    {
        // get current weapon

        // check if weapon not null
        if(true)
        {
            StopAllCoroutines();

            agent.isStopped = false;
            _attackTarget = target;

            StartCoroutine(PursueAndAttackTarget());
        }
    }

    private IEnumerator PursueAndAttackTarget()
    {
        agent.isStopped = false;
        // var weapon = currentWeapon();
        while(Vector3.Distance(transform.position, _attackTarget.transform.position) > demoAttack.Range)
        {
            // Vector3 dirTargetToThis= (transform.position - _attackTarget.transform.position).normalized;
            // Vector3 targetDestination = _attackTarget.transform.position + (dirTargetToThis * (demoAttack.Range - 0.15f));

            agent.destination = _attackTarget.transform.position;
            yield return null;
        }

        agent.isStopped = true;

        transform.LookAt(_attackTarget.transform);
        anim.SetTrigger("Attack");
    }

    public void Hit()
    {
        if(_attackTarget != null)
        {
            demoAttack.ExecuteAttack(gameObject, _attackTarget);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, threshold);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, demoAttack.Range);
    }
}
