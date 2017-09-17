using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;

public enum ColorList { None, Blue, Orange, Green, Black, Red, Purple, Yellow }

public class Item : MonoBehaviour {

    public bool Grabbed { get { return grabbed; } set { grabbed = value; } }
	public Character Owner{ get { return owner; } set { owner = value; } }
    public bool Dislodged { get { return dislodged; } set { dislodged = value; } }
    public Vector3 CarryOffset { get { return carryOffset; } set { carryOffset = value; } }
	public float CarryDistance { get { return carryDistance; } set { carryDistance = value; } }
    public Queue MyQueue { get { return myQueue; } set {myQueue = value; } }
    public HoldingSpot MyHoldingSpot{ get { return myholdingspot; }set { myholdingspot = value; }}
    public ColorList ItemColor = ColorList.None;

    private void OnEnable()
    {
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += StartGrab;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += EndGrab;
    }

    void StartGrab(object sender, InteractableObjectEventArgs e)
    {
        grabbed = true;
    }

    void EndGrab(object sender, InteractableObjectEventArgs e)
    {
        grabbed = false; 
    }

    void Start(){
        myQueue = transform.parent.transform.GetComponent<Queue>();
        owner = myQueue.Owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HoldingSpot>())
        {
            HoldingSpot hs = other.GetComponent<HoldingSpot>();
            if (!hs.Occupied)
            {
                if (!grabbed)
                {
                    if (hs.MyQueue.ItemList.Contains(this))
                    {
                        return;
                    }
                    Owner = hs.Owner;
                    MyQueue = hs.MyQueue;
                    transform.SetParent(MyQueue.transform);
                    Debug.Log("My owner is now: " + Owner.name);
                    Debug.Log("MyQueue is now: " + MyQueue.name);

                    myholdingspot = hs;
                    transform.position = myholdingspot.transform.position;
                    transform.rotation = myholdingspot.transform.rotation;
                    myholdingspot.Occupied = true;
                    if (!MyQueue.ItemList.Contains(this))
                    {
                        MyQueue.ItemList.Add(this);
                    }
                    Owner.MyQueue.ReDrawQueue();
                    Rigidbody rb = GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                    rb.rotation = Quaternion.identity;
                    rb.rotation = myholdingspot.transform.rotation;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<HoldingSpot>())
        {
            HoldingSpot hs = other.GetComponent<HoldingSpot>();
            if (!hs.Occupied)
            {
                if (!grabbed)
                {
                    if (hs.MyQueue.ItemList.Contains(this))
                    {
                        return;
                    }
                    Owner = hs.Owner;
                    MyQueue = hs.MyQueue;
                    transform.SetParent(MyQueue.transform);
                    Debug.Log("My owner is now: " + Owner.name);
                    Debug.Log("MyQueue is now: " + MyQueue.name);

                    myholdingspot = hs;
                    transform.position = myholdingspot.transform.position;
                    transform.rotation = myholdingspot.transform.rotation;
                    myholdingspot.Occupied = true;
                    if (!MyQueue.ItemList.Contains(this))
                    {
                        MyQueue.ItemList.Add(this);
                    }
                    Owner.MyQueue.ReDrawQueue();
                    Rigidbody rb = GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                    rb.rotation = myholdingspot.transform.rotation;

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.GetComponent<HoldingSpot>() && Owner)
		{
            HoldingSpot hs = other.GetComponent<HoldingSpot>();
            Debug.Log("My owner used to be: " + Owner.name);
            foreach (Item itm in Owner.MyQueue.ItemList)
            {
                Debug.Log("My owner used to have the following items: " + itm.name);
            }

            Owner.MyQueue.ItemList.Remove(this);

            foreach (Item itm in Owner.MyQueue.ItemList)
            {
                Debug.Log("But after the big tear out...my owner now has: " + itm.name);
            }
            hs.Occupied = false;
            Owner.MyQueue.ReDrawQueue();
            MyQueue = null;
            Owner = null;
            transform.parent = null;
        }
    }

    private void ReturnToSender()
    {
        if(myholdingspot)
            transform.position = myholdingspot.transform.position;
    }

    public void SetOwner(Character character){
        owner = character;
    }

    public void ChangeColor(Color color)
    {

    }

	private bool grabbed;
	private Character owner;
	private bool dislodged;
    private Vector3 carryOffset;
    private float carryDistance;
    private Queue myQueue;
    private HoldingSpot myholdingspot;
}
