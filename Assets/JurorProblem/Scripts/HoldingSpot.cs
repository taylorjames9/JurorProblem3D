using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingSpot : MonoBehaviour {



    public Character Owner;
    public Queue MyQueue;
    public bool Occupied{ get { return occupied; } set { occupied = value; }}


    private bool occupied = true;


}
