using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Bu class MainMenuState'i kontrol eder.(Controller)
public class MenuState : GameState
{
    private Dictionary<string, GameObject> _resourceObjects = new Dictionary<string, GameObject>();
    private GameObject _currentState;

    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _invabtoryButton;
    [SerializeField] private Button _arcButton;
    [SerializeField] private Button _skillButton;

    private void Start()
    {
        Init();
        _currentState = _resourceObjects["ArcState"];
    }

    private void Init()
    {
        _resourceObjects.Add("ShopState", Resources.Load<GameObject>("State/ShopState"));
        _resourceObjects.Add("InvantoryState", Resources.Load<GameObject>("State/InvantoryState"));
        _resourceObjects.Add("ArcState", Resources.Load<GameObject>("State/ArcState"));
        _resourceObjects.Add("SkillState", Resources.Load<GameObject>("State/SkillState"));
    }

    private void ChangeState(string _newStateName)
    {
        _currentState.gameObject.SetActive(false);
        _currentState = _resourceObjects[_newStateName];
        _currentState.gameObject.SetActive(true);
    }
}
