using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject Floor;
    public Character MainCharacter;
    
    

    public static GameManager Instance { get; set; }
    public bool IsTargetChoosingMode { get { return isTargetChoosingMode; } set { isTargetChoosingMode = value; } }
    public List<Character> AllCharactersInScene
    {
        get { return allCharactersInScene; }
        set { allCharactersInScene = value; }
    }

	// Use this for initialization
	void Awake () {
        Instance = this;
	}

    private void OnEnable()
    {
        GoButton.OnStartSequence += ExecuteSequence;
    }

    private void OnDisable()
    {
        GoButton.OnStartSequence -= ExecuteSequence;
    }

    public void SetFloorColor(bool black)
    {
        if (black) 
            Floor.GetComponent<Renderer>().material.color = Color.black;
        else
            Floor.GetComponent<Renderer>().material.color = Color.white;
    }

    public void ExecuteSequence()
    {
        StartCoroutine(MainCharacter.ApproachMyTarget());
        Debug.Log("Approaching my target");
    }

    public Character ConvertItemColorToTarget(ColorList cl)
    {
        Character tempChar = null;
        switch (cl)
        {
            case ColorList.Green:
                tempChar = allCharactersInScene[2];
                break;

            case ColorList.Orange:
                tempChar = allCharactersInScene[1];

                break;

            case ColorList.Black:

                break;

            case ColorList.Blue:
                tempChar = allCharactersInScene[0];

                break;

            default:

                break;
        }
        return tempChar;
    }



    [SerializeField]
    private List<Character> allCharactersInScene;

    private bool isTargetChoosingMode;

}
