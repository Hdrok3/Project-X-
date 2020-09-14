using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Bu class MainMenuState'i kontrol eder.(Controller)
public class MenuState : GameState
{
    private Dictionary<string, MainMenuState> _resourceObjects = new Dictionary<string, MainMenuState>();
    private MainMenuState _currentState;


    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _invantoryButton;
    [SerializeField] private Button _arcButton;
    [SerializeField] private Button _skillButton;

    private void Start()
    {
        Init();
        SetButtonEvents();
        _currentState = _resourceObjects["ArcState"].GetComponent<MainMenuState>();
    }

    private void Init()
    {
        _resourceObjects.Add("ShopState", Resources.Load<GameObject>("State/ShopState").GetComponent<MainMenuState>());
        _resourceObjects.Add("InvantoryState", Resources.Load<GameObject>("State/InvantoryState").GetComponent<MainMenuState>());
        _resourceObjects.Add("ArcState", Resources.Load<GameObject>("State/ArcState").GetComponent<MainMenuState>());
        _resourceObjects.Add("SkillState", Resources.Load<GameObject>("State/SkillState").GetComponent<MainMenuState>());

        ChangeState(_resourceObjects["ArcState"]);
    }

    private void ChangeState(MainMenuState newState)
    {
        _currentState?.OnStateExit();
        _currentState = newState;
        _currentState.OnStateEnter(this.gameObject.transform);
    }

    private void SetButtonEvents()
    {
        _arcButton.onClick.AddListener(OnClickArcButton);
        _invantoryButton.onClick.AddListener(OnClickInvantoryButton);
        _shopButton.onClick.AddListener(OnClickShopButton);
        _skillButton.onClick.AddListener(OnClickSkillButton);
    }

    private void OnClickShopButton()
    {
        ChangeState(_resourceObjects["ShopState"]);
    }

    private void OnClickInvantoryButton()
    {
        ChangeState(_resourceObjects["InvantoryState"]);
    }

    private void OnClickArcButton()
    {
        ChangeState(_resourceObjects["ArcState"]);
    }

    private void OnClickSkillButton()
    {
        ChangeState(_resourceObjects["SkillState"]);
    }

}
