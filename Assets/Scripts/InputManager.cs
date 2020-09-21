using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }

    public bool Moving { get; set; }

    public void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        Moving = Vertical != 0 || Horizontal != 0;
    }
}
