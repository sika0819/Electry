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
    public bool isCreateRopeStart=false;
    public bool isCreateRopeEnd=false;
    Canvas showCanvas;
    GameObject DialogBox;
    Button DialogSureBtn;
    Button DialogCancelBtn;
    bool showDialog = false;
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
        DialogBox = GameObject.Find(ResourceTool.DIALOGBOX);
        DialogSureBtn = DialogBox.transform.Find(ResourceTool.SURE_BTN).GetComponent<Button>();
        DialogSureBtn.onClick.AddListener(OnDialogSure);
        DialogCancelBtn = DialogBox.transform.Find(ResourceTool.CANCEL_BTN).GetComponent<Button>();
        DialogCancelBtn.onClick.AddListener(OnDialogCancel);
        DialogBox.SetActive(false);
        CreateElement.Instance.SetInformBox(informText);
        showCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
	}

    private void OnDialogCancel()
    {
        DialogBox.SetActive(false);
        showDialog = false;
    }

    private void OnDialogSure()
    {
        if (createdElement != null) {
            ResourceTool.DestoryGameObject(createdElement.EleObj);
            CreateElement.Instance.RemoveElement(createdElement);
            createdElement.Destory();
            createdElement = null;
        }
        DialogBox.SetActive(false);
        showDialog = false;
    }

    private void OnCreate(GameObject go)
    {
        //if (createdElement == null)
        {
            isCreateRopeStart = false;
            switch (go.name)
            {
                case ResourceTool.ENERGYBTN:
                    CreateElement.Instance.Init(ResourceTool.EnergyPreb, ElementType.Battery);
                    break;
                case ResourceTool.LINEBTN:
                    CreateElement.Instance.Init(ResourceTool.LinePreb, ElementType.Line);
                    isCreateRopeStart = true;
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
                
                if (createdElement != null&&startDraw&&!isCreateRopeStart)
                {
                    createdElement.SetPosition(hitPos);
                }
            }
            else if (hit.collider.tag == ResourceTool.PREFAB)
            {
                if (!startDraw && Input.GetMouseButtonDown(0)&&!isCreateRopeStart&&!isCreateRopeEnd)
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
                if (!startDraw && Input.GetMouseButtonDown(1))
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
                        Vector3 uiPos = ResourceTool.WorldToUIPoint(expCamera, showCanvas, hitPos);
                        //Debug.Log(uiPos);
                        DialogBox.transform.position = uiPos;
                        DialogBox.SetActive(true);
                        showDialog = true;
                    }

                    //isMove = false;
                }
            }
            else if (hit.collider.tag == ResourceTool.POINT)
            {
                if (!startDraw && Input.GetMouseButtonDown(1))
                {
                    Debug.Log(hit.collider.gameObject.transform.parent.name);
                    createdElement = CreateElement.Instance.GetElement(hit.collider.gameObject.transform.parent.name);
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
                        Vector3 uiPos = ResourceTool.WorldToUIPoint(expCamera, showCanvas, hitPos);
                        //Debug.Log(uiPos);
                        DialogBox.transform.position = uiPos;
                        DialogBox.SetActive(true);
                        showDialog = true;
                    }
                    
                    //isMove = false;
                }
            }
            if (startDraw && isMove && Input.GetMouseButtonDown(0))
            {
                startDraw = false;
                
            }else if(!showDialog&& Input.GetMouseButtonDown(0))
            {
                DialogBox.SetActive(false);
            }
        }
       
        if (!startDraw && isMove && Input.GetMouseButton(0))
        {
            if (createdElement != null)
            {
                createdElement.setShader(outlineShader);
                if (!isCreateRopeStart)
                {
                    if (createdElement.EleType != ElementType.Line)
                        createdElement.SetPosition(hitPos);
                }
                else
                {
                    if (createdElement.EleType == ElementType.Line)
                        createdElement.SetStartPos(hitPos);
                }
                
            }
        }
        if (!startDraw&&isCreateRopeEnd)
        {
            if (createdElement!=null)
            {
                if (hit.collider.tag == ResourceTool.GROUND)
                {
                    if(createdElement.EleType==ElementType.Line)
                        createdElement.SetEndPos(hitPos);
                }
                else if (hit.collider.tag == ResourceTool.POINT && !hit.collider.transform.parent.name.Contains(ResourceTool.ROPE)) {
                    if (createdElement.EleType == ElementType.Line)
                        createdElement.SetEndPos(hit.collider.gameObject.transform.position);
                }
            }
        }
        if (!startDraw && Input.GetMouseButtonUp(0))
        {
            isMove = false;
            if (createdElement != null)
            {
                createdElement.setShader(defaultShader);
                
                //createdElement = null;
            }
            if (isCreateRopeStart)
            {
                if (createdElement.EleType == ElementType.Line)
                {
                    if (hit.collider.tag == ResourceTool.POINT)
                    {
                        createdElement.SetStartPos(hit.collider.transform.position);
                        Rope tempRope = CreateElement.Instance.GetRope(createdElement.name);
                        tempRope.StartVertexObj.GetComponent<RopePoint>().SetLinkObj(hit.collider.gameObject);
                    }
                    isCreateRopeStart = false;
                    isCreateRopeEnd = true;
                }
            }
            else if(isCreateRopeEnd){
                if (createdElement.EleType == ElementType.Line)
                {
                    if (hit.collider.gameObject.tag == ResourceTool.POINT)
                    {
                        Rope tempRope = CreateElement.Instance.GetRope(createdElement.name);
                        if (tempRope != null)
                        {
                            //Debug.Log("连接尾点");
                            RopePoint ropePoint = tempRope.EndVertexObj.GetComponent<RopePoint>();
                            if (ropePoint.linkObj != null && ropePoint.linkObj != tempRope.startPoint.NodeObj)
                            {
                                Debug.Log("连接尾点" + ropePoint.linkObj);
                                createdElement.SetEndPos(ropePoint.linkObj.transform.position);
                                ropePoint.SetLinkObj(ropePoint.linkObj);
                            }
                            else {
                                createdElement.SetEndPos(hit.point);
                            }
                        }
                    }
                }
                isCreateRopeEnd = false;
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
