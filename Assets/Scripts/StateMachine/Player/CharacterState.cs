using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CharacterState : ICharState
{
    protected static PlayerController _player;
    protected static Transform _myTransform;
    public static void Init(PlayerController player)
    {
        if(_player == null)
            _player = player;
        _myTransform = _player.transform;
    }


    // Listeye eklemeler constructor ile de , fonksiyon ile de
    // hangisi daha iyi olur dusunmek gerek.
    protected List<ICharState> _transitionToStates = new List<ICharState>();
    protected Dictionary<Type, Func<bool>> _transitionConditions = new Dictionary<Type, Func<bool>>();

    public Func<bool> Condition { get; protected set; }
    public Dictionary<Type, Func<bool>> TransitionConditions => _transitionConditions;

    public ICharState ChangeState { get; protected set; }
    private ICharState idleState;

    public virtual void OnEnter()
    {
        Debug.Log("Entered: " + GetType());
        ChangeState = null;
    }

    public abstract void OnExit();

    public virtual void Update()
    {
        foreach (var transition in _transitionToStates)
        {
            if (transition.GetType() == typeof(PlayerIdleState)) idleState = transition;

            Func<bool> Condition;
            if (transition.TransitionConditions.TryGetValue(GetType(), out Condition))
            {
                if(Condition())
                {
                    ChangeState = transition;
                    return;
                }
            }
            else
            {
                Debug.LogError("Transition Condition Doesn't exit for: " + GetType() + " to " + transition.GetType());
            }
        }
    }

    public void InitTransitions(params ICharState[] states)
    {
        _transitionToStates.AddRange(states);
    }
}

public class PlayerIdleState : CharacterState
{

    public PlayerIdleState()
    {
        _transitionConditions.Add(
            typeof(PlayerAimState),
            () => { return _player.AttackTarget == null; }
            );

        _transitionConditions.Add(
            typeof(PlayerMoveState),
            () => { return !_player.InputManager.Moving && _player.AttackTarget == null && Vector3.Distance(_myTransform.position, _player.AttackTarget.transform.position) > _player.threshold; }
            );
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
    }

    public override void Update()
    {
        base.Update();
    }
}

public class PlayerMoveState : CharacterState
{
    Vector3 input;
    Vector3 movePosition;
    bool hasTarget;
    bool hasInput;
    public PlayerMoveState()
    {
        // typeof(xxx)' ten bu state'e geçiş için
        _transitionConditions.Add(
            typeof(PlayerIdleState),
            () => { CheckInput(); return hasInput; }
            );

        _transitionConditions.Add(
            typeof(PlayerAimState),
            () => { CheckInput();  return hasInput || (hasTarget && Vector3.Distance(_myTransform.position, _player.AttackTarget.transform.position) > _player.AttackRange); }
            );

        _transitionConditions.Add(
            typeof(PlayerAttackingState),
            () => { CheckInput(); return  hasInput || (hasTarget && Vector3.Distance(_myTransform.position, _player.AttackTarget.transform.position) > _player.AttackRange); }
            );
    }
    public override void OnEnter()
    {
        base.OnEnter();
        if(hasTarget)
        {
            movePosition = _player.AttackTarget.transform.position;
        }
    }

    public override void OnExit()
    {
        _player.Animator.SetFloat("Speed", 0f);
    }

    public override void Update()
    {
        if (hasInput)
        {
            input = new Vector3(_player.InputManager.Horizontal, 0, _player.InputManager.Vertical).normalized;
            movePosition = input + _myTransform.position;
        }
        MoveToTarget(movePosition);
        _player.Animator.SetFloat("Speed", _player.Agent.velocity.magnitude);

        CheckInput();

        if (!hasInput)
        {
            base.Update();
        }
    }
    public void MoveToTarget(Vector3 point)
    {
        _player.StopAllCoroutines();
        _player.Agent.isStopped = false;
        _player.Agent.SetDestination(point);
    }

    private void CheckInput()
    {
        hasInput = _player.InputManager.Moving;
        if (hasInput) _player.AttackTarget = null;
        hasTarget = _player.AttackTarget != null;
    }
}

public class PlayerAttackingState : CharacterState
{

    public PlayerAttackingState()
    {
        _transitionConditions.Add(
            typeof(PlayerMoveState),
            () => { return _player.AttackTarget != null &&  (Vector3.Distance(_myTransform.position, _player.AttackTarget.transform.position) <= _player.AttackRange); }
            );

        _transitionConditions.Add(
            typeof(PlayerAimState),
            () => {
                return (_player.AttackTarget != null && (Vector3.Distance(_myTransform.position, _player.AttackTarget.transform.position) <= _player.AttackRange)); }
            );
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _player.StartCoroutine(PursueAndAttackTarget());
    }

    public override void OnExit()
    {
        _player.Animator.SetBool("Attack", false);
    }

    public override void Update()
    {
        //if(_player.CanAttack)
        //{
        //    _player.StartCoroutine(PursueAndAttackTarget());
        //}

        base.Update();
    }


    private IEnumerator PursueAndAttackTarget()
    {
        _player.Agent.isStopped = false;
        // var weapon = currentWeapon();
        while (Vector3.Distance(_myTransform.position, _player.AttackTarget.transform.position) > _player.AttackRange) // 0 yerine demoAttack.Range
        {
            // Vector3 dirTargetToThis= (transform.position - _attackTarget.transform.position).normalized;
            // Vector3 targetDestination = _attackTarget.transform.position + (dirTargetToThis * (demoAttack.Range - 0.15f));
            _player.Agent.destination = _player.AttackTarget.transform.position;
            yield return null;
        }

        _player.Agent.isStopped = true;

        _myTransform.LookAt(_player.AttackTarget.transform);

        _player.Animator.SetBool("Attack", true);
        _player.CanAttack = false;
    }
}

public class PlayerAimState : CharacterState
{
    float findTargetRadius;
    LayerMask findTargetLayer;

    public PlayerAimState()
    {
        // if there is any enemy
        _transitionConditions.Add(
            typeof(PlayerIdleState),
            () => { return true; }
            );
        _transitionConditions.Add(
            typeof(PlayerMoveState),
            () => { return !_player.InputManager.Moving && _player.AttackTarget == null; });

        findTargetRadius = _player.threshold;
        findTargetLayer = _player.enemyLayer;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _player.AttackTarget = null;
        var enemies = Physics.OverlapSphere(_myTransform.position, findTargetRadius, findTargetLayer);

        AimTarget(enemies.FindClosestGameObject(_myTransform));
    }

    public override void Update()
    {
        base.Update();
    }
    public override void OnExit()
    {
    }

    public void AimTarget(GameObject target)
    {
        // get current weapon

        // check if weapon not null
        if (true)
        {
            _player.StopAllCoroutines();

            _player.Agent.isStopped = false;
            _player.SetAttackTarget(target);
        }
    }
}