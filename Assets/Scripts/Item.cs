using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HUX;

public class Item : MonoBehaviour {

    public bool Grabbed { get { return grabbed; } set { grabbed = value; } }
	public Character Owner{ get { return owner; } set { owner = value; } }
    public bool Dislodged { get { return dislodged; } set { dislodged = value; } }
    public Vector3 CarryOffset { get { return carryOffset; } set { carryOffset = value; } }
	public float CarryDistance { get { return carryDistance; } set { carryDistance = value; } }
    public Queue MyQueue { get { return myQueue; } set {myQueue = value; } }


    void Start(){
        myQueue = transform.parent.transform.GetComponent<Queue>();
        owner = myQueue.Owner;
    }

	void Update(){
        if (grabbed)
        {
            carryDistance = Vector3.Distance(Veil.Instance.HeadTransform.position, transform.position);
            transform.position = Veil.Instance.HeadTransform.position + (Veil.Instance.HeadTransform.forward * carryDistance);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.name.Contains(("queue"))){
        //    owner = other.GetComponent<Queue>().Owner; 
        //}
        //if(other.name.Contains("holdingspot")){
        //    Owner.MyQueue.ItemList.Insert(Owner.MyQueue.HoldingSpots.IndexOf(other.gameObject), this);
        //}

        //Owner.MyQueue.ReDrawQueue();
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.name.Contains("holdingspot") && Owner)
		{
            Debug.Log("My owner used to be: " + Owner.name);
            Debug.Log("My owner used to have the following items: " +Owner.MyQueue.ItemList);
            Owner.MyQueue.ItemList.Remove(this);
			Debug.Log("But after the big tear out...my owner now has: " + Owner.MyQueue.ItemList);
            MyQueue = null;
			Owner = null;
		}
    }

    public void SetOwner(Character character){
        owner = character;
    }

	private bool grabbed;
	private Character owner;
	private bool dislodged;
    private Vector3 carryOffset;
    private float carryDistance;
    private Queue myQueue;
}
