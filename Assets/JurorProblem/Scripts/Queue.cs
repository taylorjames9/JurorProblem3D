using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour {


    public List<Item> ItemList;
    public List<HoldingSpot> HoldingSpots;
    public Character Owner;


    public void Start(){
        ReDrawQueue();
    }

    public void ReDrawQueue()
    {
        foreach (HoldingSpot hSpot in HoldingSpots)
        {
            hSpot.Occupied = false;
        }

        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].transform.position = HoldingSpots[i].transform.position;
            ItemList[i].MyHoldingSpot = HoldingSpots[i];
            HoldingSpots[i].Occupied = true;
        }
        Debug.Log("Redrawing the queue");
    }
}
