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
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].transform.position = HoldingSpots[i].transform.position;
            HoldingSpots[i].Empty = false;
        }
        Debug.Log("Redrawing the queue");
    }
}
