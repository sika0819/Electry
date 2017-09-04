using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Experiment : MonoBehaviour {
    public Element createdElement {
        get {
            return _createElement;
        }set {
            if (_createElement != value) {
                _createElement = value;
                
            }
        }
    }
    
    private Element _createElement;
    bool isMove = false;
    bool isEnterArea;
    GameObject[] expButtonsObj;
    Toggle[] expButton;
    Material mat;
    Camera expCamera;
    public Shader outlineShader;
    public Shader defaultShader;
    Text informText;
    Vector3 hitPos;
    // Use this for initialization
    void Start () {
        
        expButtonsObj = GameObject.FindGameObjectsWithTag(ResourceTool.BTNTAG);
        expButton = new Toggle[expButtonsObj.Length];
        for (int iLoop = 0; iLoop < expButtonsObj.Length; iLoop++)
        {
            expButton[iLoop] = expButtonsObj[iLoop].GetComponent<Toggle>();
            EventTriggerListener.Get(expButtonsObj[iLoop]).onUp = OnCreate;
        }
        expCamera = GameObject.FindGameObjectWithTag(ResourceTool.EXPCAMERA).GetComponent<Camera>();
        informText = GameObject.Find(ResourceTool.INFORM_TEXT).GetComponent<Text>();
        CreateElement.Instance.SetInformBox(informText);
	}

    private void OnCreate(GameObject go)
    {
        switch (go.name)
        {
            case ResourceTool.ENERGYBTN:
                CreateElement.Instance.Init(ResourceTool.EnergyPreb,ElementType.Battery);
                break;
            case ResourceTool.LINEBTN:
                CreateElement.Instance.Init(ResourceTool.LinePreb,ElementType.Line);
                break;
            case ResourceTool.SWICTHBTN:
                CreateElement.Instance.Init(ResourceTool.SwitchPreb,ElementType.Switch);
                break;
            case ResourceTool.RESISTANCEBTN:
                CreateElement.Instance.Init(ResourceTool.ResistancePreb,ElementType.Resistance);
                break;
            case ResourceTool.LIGHTBTN:
                CreateElement.Instance.Init(ResourceTool.LightPreb,ElementType.Light);
                break;
            case ResourceTool.VOLTMETERBTN:
                CreateElement.Instance.Init(ResourceTool.VoltmeterPreb, ElementType.Voltmeter);
                break;
        }
        createdElement=CreateElement.Instance.Ele;
        if (createdElement != null)
        {
            mat = createdElement.EleObj.GetComponentInChildren<Renderer>().sharedMaterial;
            defaultShader = mat.shader;
            isMove = true;
        }
        
    }

    RaycastHit hit;
    // Update is called once per frame
    void Update () {
        Ray ray = expCamera.ScreenPointToRay(Input.mousePosition);
       
        if (Physics.Raycast(ray, out hit))
        {
            if (isEnterArea && hit.collider.tag == ResourceTool.GROUND)
            {
                hitPos = hit.point;
            }
            else if(hit.collider.tag == ResourceTool.PREFAB)
            {
                if (isEnterArea&&Input.GetMouseButtonDown(0)&&createdElement==null)
                {
                    createdElement = CreateElement.Instance.GetElement(hit.collider.name);
                    createdElement.setShader(outlineShader);
                    if (createdElement != null)
                    {
                        if (createdElement.EleType == ElementType.Switch)
                        {
                            Debug.Log(createdElement.name);
                            EleSwitch seleSwitch = CreateElement.Instance.GetSwitch(createdElement.name);
                            seleSwitch.ToggleTurn();
                        }
                    }
                    isMove = true;
                }
            }
           
        }
        if (isMove && Input.GetMouseButton(0))
        {
            if (createdElement != null)
            {
                createdElement.SetPosition(hitPos);
            }
        }
        CreateElement.Instance.Update();
    }
    public void OnPointerEnter() {
        isEnterArea = true;
    }
    public void OnPointerUp() {
        if (isEnterArea)
        {
            if (createdElement != null)
            {
                createdElement.setShader(defaultShader);
                createdElement = null;
                isMove = false;
            }
        }
    }
    public void OnPointExit() {
        if (createdElement != null)
        {
            isEnterArea = false;
            createdElement.setShader(defaultShader);
            createdElement = null;
            isMove = false;
        }
    }
    void OnApplicationQuit() {
        CreateElement.Instance.OnDestory();
        GenrateIndex.Instance.OnDestory();
    }
}
