using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine
{
    ICharState _currentState;
    PlayerController _player;

    Dictionary<System.Type, ICharState> _allStates = new Dictionary<System.Type, ICharState>();

    public CharacterStateMachine(PlayerController player)
    {
        _player = player;
        CharacterState.Init(player);

        PlayerIdleState idleState = new PlayerIdleState();
        PlayerMoveState moveState = new PlayerMoveState();
        PlayerAimState aimState = new PlayerAimState();
        PlayerAttackingState attackState = new PlayerAttackingState();

        idleState.InitTransitions(moveState, aimState);
        moveState.InitTransitions(idleState, attackState, aimState);
        aimState.InitTransitions(moveState, attackState, idleState);
        attackState.InitTransitions(moveState);

        _allStates.Add(idleState.GetType(), idleState);
        _allStates.Add(moveState.GetType(), moveState);

        _currentState = idleState;
    }

    public void Update()
    {
        _currentState.Update();
        if(_currentState.ChangeState != null)
        {
            ChangeState(_currentState.ChangeState);
        }
    }
    private void ChangeState(ICharState newState)
    {
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }
}
