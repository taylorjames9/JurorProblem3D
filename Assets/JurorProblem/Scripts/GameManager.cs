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

    public void SetFloorColor(bool black)
    {
        if (black) 
            Floor.GetComponent<Renderer>().material.color = Color.black;
        else
            Floor.GetComponent<Renderer>().material.color = Color.white;
    }



    [SerializeField]
    private List<Character> allCharactersInScene;

    private bool isTargetChoosingMode;

}
