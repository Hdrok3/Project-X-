using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject go;
    System.Action TestAction;
    void Start()
    {
        if (go != null)
        {
            TestAction += go.GetComponent<Test>().TestFunc;
            Invoke("TestAction", 3f);
        }
    }

    private void Update()
    {
        if(Time.time > 5f)
        {
            go = null;
            TestAction?.Invoke();
        }
    }

    void TestFunc()
    {
        Debug.Log(name);
    }
}
