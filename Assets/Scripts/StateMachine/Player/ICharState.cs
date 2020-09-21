using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICharState
{
    ICharState ChangeState { get; }

    Dictionary<Type, Func<bool>> TransitionConditions { get; }
    void OnEnter();
    void Update();
    void OnExit();
    void InitTransitions(params ICharState[] transitions);
}
