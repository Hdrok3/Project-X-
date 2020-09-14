using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainMenuState :  MonoBehaviour
{
    private GameObject go = null;
    protected virtual void Start()
    {
    }

    public virtual void OnStateEnter(Transform transform)
    {
        if(!go)
        {
            go = Instantiate(this.gameObject, transform);
            return;
        }

        go.SetActive(!go.activeSelf);

    }

    public virtual void OnStateExit()
    {
        go.SetActive(!go.activeSelf);
    }

}

