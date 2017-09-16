using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Character : MonoBehaviour {

    //public Queue MyQueue{ get { return myQueue; } set { myQueue = value; }}
    public Queue MyQueue;
    public float Speed { get { return speed; } set { speed = value; } }
    public GameObject MyBasePointer;
    public GameObject MyBody;
    public Character Target
    {
        get { return target; }
        set { target = value; }
    }


    private void OnEnable()
    {
        if (isMain)
            MyBody.GetComponent<VRTK_InteractableObject>().InteractableObjectTouched += ToggleTargetSelectionMode;
        MyBody.GetComponent<VRTK_InteractableObject>().InteractableObjectTouched += MakeMeTheTarget;
    }

    private void OnDisable()
    {
        if (isMain)
            MyBody.GetComponent<VRTK_InteractableObject>().InteractableObjectTouched -= ToggleTargetSelectionMode;
        MyBody.GetComponent<VRTK_InteractableObject>().InteractableObjectTouched -= MakeMeTheTarget;

    }

    void Start(){
        myHomeBase = transform.position;
        if (isMain)
        {
            FaceMyTarget(ChooseRandomTarget());
        }
    }

    public void ToggleTargetSelectionMode(object sender, InteractableObjectEventArgs e)
    {
        if (isMain && MyBasePointer)
        {
            GameManager.Instance.IsTargetChoosingMode = !GameManager.Instance.IsTargetChoosingMode;
            basePointerVisible = GameManager.Instance.IsTargetChoosingMode;
            MyBasePointer.SetActive(GameManager.Instance.IsTargetChoosingMode);
            GameManager.Instance.SetFloorColor(GameManager.Instance.IsTargetChoosingMode);
            //if (GameManager.Instance.IsTargetChoosingMode)
            //{
            //    FaceMyTarget(IncrementTarget());
            //}
        }
    }

    public void MakeMeTheTarget(object sender, InteractableObjectEventArgs e)
    {
        if (GameManager.Instance.IsTargetChoosingMode)
        {
            if (!isMain)
            {
                GameManager.Instance.MainCharacter.Target = this;
            }
            GameManager.Instance.MainCharacter.FaceMyTarget();
        }
    }

    public void FaceMyTarget(Character targ){
        transform.LookAt(targ.transform.position);
        transform.Rotate(0, transform.rotation.y, 0);
        //StartCoroutine(ApproachMyTarget());
    }

    public void FaceMyTarget()
    {
        transform.LookAt(target.transform.position);
        transform.Rotate(0, transform.rotation.y, 0);
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

    public void GetStabbed(){
        
    }

    private Character ChooseRandomTarget()
    {
        int rand = Random.Range(0, GameManager.Instance.AllCharactersInScene.Count);
        Debug.Log("Rand value = " + rand);
        target = GameManager.Instance.AllCharactersInScene[rand];
        return target != this ? target : ChooseRandomTarget();  
    }

    private Character IncrementTarget()
    {
        int val = GameManager.Instance.AllCharactersInScene.IndexOf(target);
        val++;
        if(val > GameManager.Instance.AllCharactersInScene.Count - 1)
        {
            val = 0;
        }
        Debug.Log("target val = " + val);
        target = GameManager.Instance.AllCharactersInScene[val];
        return target != this ? target : IncrementTarget();
    }

    [SerializeField]
    private bool isMain;
    private Color myColor;
    private Character target;
    private Vector3 myHomeBase;
    private float speed;
    private bool basePointerVisible;

}
