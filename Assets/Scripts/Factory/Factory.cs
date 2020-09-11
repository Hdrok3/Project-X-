using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory
{
    protected static Dictionary<System.Type, Factory> _allFactories = new Dictionary<System.Type, Factory>();

    public static Dictionary<System.Type, Factory> AllFactories
    {
        get { return _allFactories; }
    }

    public Factory()
    {
        if (!_allFactories.ContainsKey(GetType()))
        {
            _allFactories.Add(GetType(), this);
        }
    }
}
