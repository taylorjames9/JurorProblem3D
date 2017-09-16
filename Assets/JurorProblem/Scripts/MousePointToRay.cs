using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointToRay : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.1f, true);
            if (hit.transform != null)
            {
                Debug.Log("name of hit " + hit.transform.name);
                if (hit.transform.GetComponent<Item>() != null)
                {
                    hit.transform.GetComponent<Item>().Grabbed = true;
                    //hit.transform.GetComponent<Item>().CarryOffset = hit.transform.GetComponent<Item>().transform.position - HUX.Veil.Instance.HeadTransform.position;
                    //hit.transform.GetComponent<Item>().CarryDistance = Vector3.Distance(HUX.Veil.Instance.HeadTransform.position, hit.transform.GetComponent<Item>().transform.position);
                    Debug.Log("Item name is : " + hit.transform.name + " " + "grabbed state is: " + hit.transform.GetComponent<Item>().Grabbed);
                    Debug.Log("Carry Offset is set to: " + hit.transform.GetComponent<Item>().CarryOffset);
                }
            }
        }
	}
}
