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
    public HoldingSpot MyHoldingSpot{ get { return myholdingspot; }set { myholdingspot = value; }}


    void Start(){
        myQueue = transform.parent.transform.GetComponent<Queue>();
        owner = myQueue.Owner;
    }

	void Update(){
        if (grabbed)
        {
            //carryDistance = Vector3.Distance(Veil.Instance.HeadTransform.position, transform.position);
            transform.position = Veil.Instance.HeadTransform.position + (Veil.Instance.HeadTransform.forward * carryDistance);
            //Owner.MyQueue.ReDrawQueue();

        }
        if(Input.GetKeyDown(KeyCode.Space)){
            grabbed = false;
            //Owner.MyQueue.ReDrawQueue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HoldingSpot>() && Owner)
        {
            if (other.GetComponent<HoldingSpot>().Empty)
            {

                grabbed = false;
                Owner = other.GetComponent<HoldingSpot>().Owner;
                MyQueue = other.GetComponent<HoldingSpot>().MyQueue;
                transform.SetParent(MyQueue.transform);
                Debug.Log("My owner is now: " + Owner.name);
                Debug.Log("MyQueue is now: " + MyQueue.name);

                myholdingspot = other.GetComponent<HoldingSpot>();
                transform.position = myholdingspot.transform.position;
                transform.rotation = myholdingspot.transform.rotation;
                if (!MyQueue.ItemList.Contains(transform.GetComponent<Item>()))
                {
                    MyQueue.ItemList.Add(this);
                }
            }
        }

        Owner.MyQueue.ReDrawQueue();

    }

    private void OnTriggerExit(Collider other)
    {
		if (other.GetComponent<HoldingSpot>() && Owner)
		{
            Debug.Log("My owner used to be: " + Owner.name);
            Debug.Log("My owner used to have the following items: " +Owner.MyQueue.ItemList);
            Owner.MyQueue.ItemList.Remove(this);
			Debug.Log("But after the big tear out...my owner now has: " + Owner.MyQueue.ItemList);
            //MyQueue = null;
			//Owner = null;
            //transform.parent = null;
            //Owner.MyQueue.ReDrawQueue();
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
    private HoldingSpot myholdingspot;
}
