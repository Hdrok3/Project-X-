using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StateController
{
    private Factory _factory;
    private Dictionary<string, GameObject> _resourceObjects = new Dictionary<string, GameObject>();
    private GameState _currentState = null;
    public StateController()
    {
        //_factory = new MenuStateFactory();
        Init();
        ChangeState();
    }

    void ChangeState()
    {


    }

    void Print(GameObject go)
    {
        Debug.Log(go);
    }

    private void Init()
    {
        _resourceObjects.Add("ShopState", Resources.Load<GameObject>("State/ShopState"));
        _resourceObjects.Add("InvantoryState", Resources.Load<GameObject>("State/InvantoryState"));
        _resourceObjects.Add("ArcState", Resources.Load<GameObject>("State/ArcState"));
        _resourceObjects.Add("SkillState", Resources.Load<GameObject>("State/SkillState"));
    }

    void Test()
    {

        //_System.Linq ile dictionary'de gezinmek. ForEach action alıyor. GameObject alan bir metod verilebilir.
        //_resourceObjects.Values.ToList().ForEach((x) => { x.transform.position = Vector3.zero;}); 

        //System.Linq olmadan dictionary'de gezinmek.
        //foreach(var item in _resourceObjects.Values)
        //{
        //    Debug.Lo(item.name);
        //}

        //System.Linq olmadan dictionary'de KeyPair ile gezinmek.
        //foreach(var kp in _resourceObjects)
        //{
        //    Debug.Log(kp.Value);
        //}
    }
}
