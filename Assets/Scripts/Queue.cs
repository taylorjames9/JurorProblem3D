using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour {


    public List<Item> ItemList;
    public List<GameObject> HoldingSpots;
    public Character Owner;



    public void Start(){
        ReDrawQueue();
    }

    public void ReDrawQueue(){
        for (int i = 0; i < ItemList.Count;i++){
            if (ItemList[i] != null)
            {
                ItemList[i].transform.position = HoldingSpots[i].transform.position;
            }
        }
    }
}
