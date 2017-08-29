using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public Queue MyQueue{ get { return myQueue; } set { myQueue = value; }}
    public float speed;

    void Start(){
        myHomeBase = transform.position;
    }

    public void FaceMyTarget(){
        transform.LookAt(target.transform.position);
        transform.Rotate(0, transform.rotation.y, 0);
        StartCoroutine(ApproachMyTarget());
    }

    public IEnumerator ApproachMyTarget(){

        float dist = Vector3.Distance(transform.position, target.transform.position);
        while (dist < 0.25f){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            yield return null;
        }

        yield return null;
    }

    public void StabMyTarget(){
        
    }

    public void FaceMyHome(){
        
    }

    public void WalkHome(){
        
    }

    private Queue myQueue;
    private bool isMain;
    private Color myColor;
    private Character target;
    private Vector3 myHomeBase;
}
