using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingSpot : MonoBehaviour {



    public Character Owner;
    public Queue MyQueue;
    public bool Empty{ get { return empty; } set { empty = value; }}


    private bool empty = true;


}
