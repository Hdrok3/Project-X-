using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // TODO:
    // Burasi değiştirilecek, weapon içerisine koyulacak, weapon attack.
    [SerializeField] private WeaponAttack demoAttack;
    public LayerMask enemyLayer;
    public float threshold = 2f;

    private GameObject _attackTarget;

    NavMeshAgent agent;
    Animator anim;
    Rigidbody rb;
    CharacterStateMachine stateMachine;
    InputManager keyboardAndMouseInput;

    public Animator Animator => anim;
    public NavMeshAgent Agent => agent;
    public InputManager InputManager => keyboardAndMouseInput;
    public GameObject AttackTarget { get => _attackTarget; set => _attackTarget = value; }
    public float AttackRange => demoAttack.Range;

    public bool CanAttack { get; set; }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        keyboardAndMouseInput = GetComponent<InputManager>();

        stateMachine = new CharacterStateMachine(this);


        var testColls = Physics.OverlapSphere(transform.position, 50, enemyLayer);
        var go = testColls.FindClosestGameObject(transform);

        Debug.Log(go.name);
    }

    private void Update()
    {
        stateMachine.Update();
    }


    public void Hit()
    {
        if(_attackTarget != null)
        {
            demoAttack.ExecuteAttack(gameObject, _attackTarget);
        }
    }

    public void AttackFinished()
    {
        CanAttack = true;
    }

    public void SetAttackTarget(GameObject target)
    {
        _attackTarget = target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, threshold);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, demoAttack.Range);
    }
}
