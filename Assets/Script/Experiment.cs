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
    Camera expCamera;
    public Shader outlineShader;
    public Shader defaultShader;
    Vector3 hitPos;
    // Use this for initialization
    void Start () {
        
        expButtonsObj = GameObject.FindGameObjectsWithTag(ResourceTool.BTNTAG);
        expButton = new Toggle[expButtonsObj.Length];
        for (int iLoop = 0; iLoop < expButtonsObj.Length; iLoop++)
        {
            expButton[iLoop] = expButtonsObj[iLoop].GetComponent<Toggle>();
            EventTriggerListener.Get(expButtonsObj[iLoop]).onSelect = OnCreate;
        }
        expCamera = GameObject.FindGameObjectWithTag(ResourceTool.EXPCAMERA).GetComponent<Camera>();
	}

    private void OnCreate(GameObject go)
    {
        switch (go.name)
        {
            case ResourceTool.ENERGY:
                CreateElement.Instance.Init(ResourceTool.EnergyPreb,ElementType.Battery);
                break;
            case ResourceTool.LINE:
                CreateElement.Instance.Init(ResourceTool.LinePreb,ElementType.Line);
                break;
            case ResourceTool.SWICTH:
                CreateElement.Instance.Init(ResourceTool.SwitchPreb,ElementType.Switch);
                break;
            case ResourceTool.RESISTANCE:
                CreateElement.Instance.Init(ResourceTool.ResistancePreb,ElementType.Resistance);
                break;
            case ResourceTool.LIGHT:
                CreateElement.Instance.Init(ResourceTool.LightPreb,ElementType.Light);
                break;
        }
        createdElement=CreateElement.Instance.Ele;
        Material mat = createdElement.EleObj.GetComponentInChildren<Renderer>().sharedMaterial;
        defaultShader = mat.shader;
        isMove = true;
        
    }


    // Update is called once per frame
    void Update () {
        Ray ray = expCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == ResourceTool.GROUND)
            {
                hitPos = hit.point;
                if (isEnterArea&&isMove && Input.GetMouseButton(0))
                {
                    if (createdElement != null)
                        createdElement.SetPosition(hitPos);
                }
            }
            else if(hit.collider.tag == ResourceTool.PREFAB)
            {
                if (isEnterArea&&Input.GetMouseButtonDown(0))
                {
                    createdElement = CreateElement.Instance.GetElement(hit.collider.name);
                    isMove = true;
                }
            }
           
        }
        CreateElement.Instance.UpdateElectry();
    }
    public void OnPointerEnter() {
        isEnterArea = true;
    }
    public void OnPointerUp() {
        createdElement = null;
        isMove = false;
    }
    public void OnPointExit() {
        isEnterArea = false;
    }
    void OnApplicationQuit() {
        CreateElement.Instance.OnDestory();
        GenrateIndex.Instance.OnDestory();
    }
}
