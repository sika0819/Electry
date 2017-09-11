using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum State
{
    Wait,
    Menu,
    Test
}
public class MainController : MonoBehaviour {
    public State MenuState {
        get {
            return menuState;
        }set {
            if (menuState != value)
            {
                menuState = value;
                HideAllUI();
                switch (menuState)
                {
                    case State.Wait:
                        WaitUIObj.SetActive(true);
                        break;
                    case State.Menu:
                        MainUIObj.SetActive(true);
                        break;
                    case State.Test:
                        ExpermentUIObj.SetActive(true);
                        break;
                }
            }
        }
    }
    private State menuState;
    public GameObject WaitUIObj;
    public GameObject MainUIObj;
    public GameObject ExpermentUIObj;
    public static MainController Instance;
    
	// Use this for initialization
	void Start () {
        Instance = this;
        MenuState = State.Wait;
        ResourceTool.InitResources();
	}
	
	// Update is called once per frame
	void Update () {
       
	}
    void HideAllUI()
    {
        GameObject[] allUI = GameObject.FindGameObjectsWithTag("UI");
        for (int iloop = 0; iloop < allUI.Length; iloop++)
        {
            allUI[iloop].SetActive(false);
        }
    }
    public void OnStartMenu() {
        MenuState = State.Menu;
    }
    public void OnExperment() {
        MenuState = State.Test;
    }
  
}
