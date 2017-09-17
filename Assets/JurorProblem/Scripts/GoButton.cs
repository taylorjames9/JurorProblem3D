using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButton : MonoBehaviour {

    public static GoButton Instance;

    public delegate void StartSquenceAction();
    public static event StartSquenceAction OnStartSequence;
    public bool OnOff;

    void OnEnable()
    {
        Instance = this;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Equals("Floor"))
        {
            OnStartSequence();
        }
    }


}
