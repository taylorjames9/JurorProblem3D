using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public bool Grabbed { get { return grabbed; } set { grabbed = value; } }
	public Character Owner{ get { return owner; } set { owner = value; } }
    public bool Dislodged { get { return dislodged; } set { dislodged = value; } }


    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains(("queue"))){
            owner = other.GetComponent<Queue>().Owner; 
        }
        if(other.name.Contains("holdingspot")){
            Owner.MyQueue.ItemList.Insert(Owner.MyQueue.HoldingSpots.IndexOf(other.gameObject), this);
        }

        Owner.MyQueue.ReDrawQueue();
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.name.Contains("holdingspot"))
		{
            Owner.MyQueue.ItemList.Remove(this);
		}
    }



    public void SetOwner(Character character){
        owner = character;
    }

	private bool grabbed;
	private Character owner;
	private bool dislodged;
}
