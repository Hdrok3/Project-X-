using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Fields
    private static GameController _instance;
    private StateController _stateController;
    public GameController Instance
    {
        get { return _instance; }
    }
    #endregion

    void Awake()
    {
        _instance = this;
        _stateController = new StateController();
    }
}
