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
    public bool startDraw;
    GameObject[] expButtonsObj;
    Button[] expButton;
    Camera expCamera;
    List<Material> matArray;
    public Shader outlineShader;
    public Shader[] defaultShader;
    Text informText;
    Vector3 hitPos;
    public float rotateSpeed=10;
    // Use this for initialization
    void Start () {
        
        expButtonsObj = GameObject.FindGameObjectsWithTag(ResourceTool.BTNTAG);
        expButton = new Button[expButtonsObj.Length];
        for (int iLoop = 0; iLoop < expButtonsObj.Length; iLoop++)
        {
            expButton[iLoop] = expButtonsObj[iLoop].GetComponent<Button>();
            EventTriggerListener.Get(expButtonsObj[iLoop]).onUp = OnCreate;
        }
        expCamera = GameObject.FindGameObjectWithTag(ResourceTool.EXPCAMERA).GetComponent<Camera>();
        informText = GameObject.Find(ResourceTool.INFORM_TEXT).GetComponent<Text>();
        CreateElement.Instance.SetInformBox(informText);
	}

    private void OnCreate(GameObject go)
    {
        if (createdElement == null)
        {
            switch (go.name)
            {
                case ResourceTool.ENERGYBTN:
                    CreateElement.Instance.Init(ResourceTool.EnergyPreb, ElementType.Battery);
                    break;
                case ResourceTool.LINEBTN:
                    CreateElement.Instance.Init(ResourceTool.LinePreb, ElementType.Line);
                    break;
                case ResourceTool.SWICTHBTN:
                    CreateElement.Instance.Init(ResourceTool.SwitchPreb, ElementType.Switch);
                    break;
                case ResourceTool.RESISTANCEBTN:
                    CreateElement.Instance.Init(ResourceTool.ResistancePreb, ElementType.Resistance);
                    break;
                case ResourceTool.LIGHTBTN:
                    CreateElement.Instance.Init(ResourceTool.LightPreb, ElementType.Light);
                    break;
                case ResourceTool.VOLTMETERBTN:
                    CreateElement.Instance.Init(ResourceTool.WanYongBiaoPreb, ElementType.Voltmeter);
                    break;
            }
            createdElement = CreateElement.Instance.Ele;
            if (createdElement != null)
            {
                isMove = true;
                startDraw = true;
                matArray = new List<Material>();
                Renderer[] renderArray = createdElement.EleObj.GetComponentsInChildren<Renderer>();
                for (int i = 0; i < renderArray.Length; i++)
                {
                    for (int j = 0; j < renderArray[i].materials.Length; j++)
                    {
                        matArray.Add(renderArray[i].materials[j]);
                    }
                }
                defaultShader = new Shader[matArray.Count];
                for (int i = 0; i < matArray.Count; i++)
                {
                    defaultShader[i] = matArray[i].shader;
                }
            }
        }
    }

    RaycastHit hit;
    // Update is called once per frame
    void Update () {
        Ray ray = expCamera.ScreenPointToRay(Input.mousePosition);
       
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == ResourceTool.GROUND)
            {
                hitPos = hit.point;
                if (createdElement != null)
                {
                    createdElement.SetPosition(hitPos);
                }
            }
            else if (hit.collider.tag == ResourceTool.PREFAB)
            {
                if (!startDraw && Input.GetMouseButtonDown(0) && createdElement == null)
                {
                    createdElement = CreateElement.Instance.GetElement(hit.collider.name);

                    if (createdElement != null)
                    {
                        matArray = new List<Material>();
                        Renderer[] renderArray = createdElement.EleObj.GetComponentsInChildren<Renderer>();
                        for (int i = 0; i < renderArray.Length; i++)
                        {
                            for (int j = 0; j < renderArray[i].materials.Length; j++)
                            {
                                matArray.Add(renderArray[i].materials[j]);
                            }
                        }
                        defaultShader = new Shader[matArray.Count];
                        for (int i = 0; i < matArray.Count; i++)
                        {
                            defaultShader[i] = matArray[i].shader;
                        }
                        createdElement.setShader(outlineShader);
                        if (createdElement.EleType == ElementType.Switch)
                        {
                            EleSwitch seleSwitch = CreateElement.Instance.GetSwitch(createdElement.name);
                            seleSwitch.ToggleTurn();
                        }
                    }
                    isMove = true;
                }
            }
            else if (hit.collider.tag == ResourceTool.ROPENODE) {
                createdElement = CreateElement.Instance.GetElement(hit.collider.gameObject.transform.parent.parent.name);
            }
            if (startDraw && Input.GetMouseButtonDown(0))
            {
                startDraw = false;
                createdElement = null;
            }
        }
       
        if (!startDraw && isMove && Input.GetMouseButton(0))
        {
            if (createdElement != null)
            {
                createdElement.setShader(outlineShader);
                createdElement.SetPosition(hitPos);
            }
        }
        if (!startDraw&&isMove && Input.GetMouseButtonUp(0))
        {
            isMove = false;
            if (createdElement != null)
            {
                createdElement.setShader(defaultShader);
                createdElement = null;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (createdElement != null)
            {
                Vector3 rotateAngle = createdElement.EleObj.transform.eulerAngles;
                rotateAngle = new Vector3(rotateAngle.x, rotateAngle.y + rotateSpeed * Input.mouseScrollDelta.y, rotateAngle.z);
                createdElement.SetRotation(rotateAngle);
            }
        }
       
        CreateElement.Instance.Update();
    }

    void OnApplicationQuit() {
        CreateElement.Instance.OnDestory();
        GenrateIndex.Instance.OnDestory();
    }
}
