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
    public bool MyAssailant { get { return myAssailant; } set { myAssailant = value; } }
    public bool Alive { get { return alive; } set { alive = value; } }

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
        attackingCurrently = true;
        Debug.Log("MyTarget is : " + Target);
        float dist = Vector3.Distance(transform.position, target.transform.position);
        Debug.Log("dist to my targ is : " + dist);
        while (dist > 0.25f){
            Debug.Log("we're inside the while loop");
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            yield return 0;
        }

        yield return null;
    }

    public IEnumerator ApproachMyTarget(Character targ)
    {
        float dist = Vector3.Distance(transform.position, targ.transform.position);
        while (dist > 0.2f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targ.transform.position, step);
            yield return null;
        }

        yield return null;
    }

    public IEnumerator HandOffItem(Item itm){
        Debug.Log("Handing off an item " + itm.name);
        StartCoroutine(Target.ReceiveAnItem(itm));
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(FaceMyHome());
        StartCoroutine(WalkHome());
    }

    public IEnumerator ReceiveAnItem(Item itm)
    {
        if (itm.ItemColor.Equals(ColorList.Black))
        {
            StartCoroutine(Die());
            yield return null;
        }
        else
        {
            Target = GameManager.Instance.ConvertItemColorToTarget(itm.ItemColor);
        }
        yield return null;
    }

    public IEnumerator FaceMyHome(){
        // transform.LookAt(target.transform.position);
        // transform.Rotate(0, transform.rotation.y, 0);
        yield return null;
    }

    public IEnumerator WalkHome(){

        float dist = Vector3.Distance(transform.position, myHomeBase);
        while (dist > 0.1f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, myHomeBase, step);
            yield return null;
        }
        yield return null;
    }

    public IEnumerator Die()
    {
        yield return null;
    }

    private Character ChooseRandomTarget()
    {
        int rand = Random.Range(0, GameManager.Instance.AllCharactersInScene.Count);
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


    private void OnTriggerEnter(Collider other)
    {
        if (attackingCurrently)
        {
            if (other.GetComponent<Character>())
            {
                if (other.GetComponent<Character>().target)
                {
                    StartCoroutine(HandOffItem(MyQueue.ItemList[0]));
                }
            }
        }



        if (other.GetComponent<Character>())
        {
            if (other.GetComponent<Character>().MyAssailant)
            {

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }




    [SerializeField]
    private bool isMain;
    private bool alive;
    private bool myAssailant;
    private bool attackingCurrently;
    private bool beingAttackedCurrently;
    private Color myColor;
    private Character target;
    private Vector3 myHomeBase;
    private float speed = 0.4f;
    private bool basePointerVisible;

}
