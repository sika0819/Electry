using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element{//电子元器件基类
    private ElecEdge electryElement;//边节点
    private Vertex pos;//正极点
    private Vertex negative;//负极点
    private Edge insideEdge;
    private Node point1;
    private Node point2;
    private ElementType elementType;
    
    private GameObject eleObj;
    private GameObject consoleArea;
    #region public Property
    public string name {
        get {
            return eleObj.name;
        }
    }
    public float Currency
    {
        get
        {
            return pos.Electry;
        }
    }
    public float Resistance
    {
        get
        {
            return electryElement.Resistance;
        }
        set
        {
            electryElement.Resistance = value;
        }
    }
    public GameObject EleObj//电子元器件物体
    {
        get
        {
            return eleObj;
        }
        set
        {
            eleObj = value;
        }
    }
    public ElecEdge ElectryEdge
    {//返回元器件
        get
        {
            return electryElement;
        }set {
            electryElement = value;
        }
    }
    public Edge LineEdge {
        get {
            return insideEdge;
        }set {
            insideEdge = value;
        }
    }
    public Node Point1 {
        get {
            return insideEdge.either();
        }
    }
    public Node Point2 {
        get {
            return insideEdge.other(point1);
        }
    }
    public GameObject StartVertexObj
    {
        get
        {
            if(startVertexObj != null)
            return startVertexObj;
            Debug.LogError("获取点程序错误");
            return null;
        }
        set
        {
            if (value != null)
            {
                startVertexObj = value;
            }
            else {
                Debug.LogError("获取点程序错误");
            }
        }
    }
    private GameObject startVertexObj;
    public GameObject EndVertexObj
    {
        get
        {
            if(endVertexObj != null)
            return endVertexObj;
            Debug.LogError("获取点程序错误");
            return null;
        }
        set
        {
            if (value != null)
            {
                endVertexObj=value;
            }
            else {
                Debug.LogError("获取点程序错误");
            }
        }
    }
    private GameObject endVertexObj;
    #endregion
    public Element() {
        consoleArea = GameObject.Find(ResourceTool.CONSOLEAREA);
    }
    public void Init(Element copyEle) {
        this.eleObj = copyEle.EleObj;
        this.electryElement = copyEle.ElectryEdge;
        this.pos = copyEle.Pos;
        this.negative = copyEle.Negative;
        this.elementType = copyEle.thisType;
    }
    public void Init(GameObject obj,int i)
    {
        eleObj = ResourceTool.InstitateGameObject(obj);
        eleObj.name = obj.name + i;
        eleObj.transform.SetParent(consoleArea.transform);
        
    }

    public virtual void Electry() {

    }
    public void setCurrency(float current) {//设置电流
        pos.setElectry(current);
        negative.setElectry(current);
    }
    
   
    public void SetResistance(float value) {
        Resistance = value;
    }
    public void InitEleType(ElementType initType)
    {
        elementType = initType;
        if (initType != ElementType.Line)
        {
            Debug.Log("加入顶点");
            pos = new Vertex(GenrateIndex.Instance.Index);
            negative = new Vertex(GenrateIndex.Instance.Index);
            point1 = new Node(pos.index);
            point2 = new Node(negative.index);
            electryElement = new ElecEdge(pos, negative);//从正极到负极建立一个有向边
            electryElement.Resistance = 0;
            electryElement.name = name;
            insideEdge = new Edge(point1, point2); 
        }
        else {
            pos = new Vertex();
            negative = new Vertex();
        }
        
        if (eleObj.transform.FindChild(ResourceTool.STARTPOINT))
        {
            StartVertexObj = eleObj.transform.FindChild(ResourceTool.STARTPOINT).gameObject;
        }
        else
        {
            StartVertexObj = new GameObject();
            StartVertexObj.transform.SetParent(eleObj.transform);
            StartVertexObj.name = ResourceTool.STARTPOINT;
        }
        if (eleObj.transform.FindChild(ResourceTool.ENDPOINT))
        {
            EndVertexObj = eleObj.transform.FindChild(ResourceTool.ENDPOINT).gameObject;
        }
        else
        {
            EndVertexObj = new GameObject();
            EndVertexObj.transform.SetParent(eleObj.transform);
            EndVertexObj.name = ResourceTool.ENDPOINT;
        }
        if (initType != ElementType.Line)
        {
            StartVertexObj.name = ResourceTool.STARTPOINT + pos.index;
            EndVertexObj.name = ResourceTool.ENDPOINT + negative.index;
        }
    }
    public void SetPoint(Vector3 start, Vector3 end)
    {
        StartVertexObj.transform.localPosition = start;
        EndVertexObj.transform.localPosition = end;
    }
    public void SetPosition(Vector3 pos)
    {
        eleObj.transform.localPosition = pos;
    }
    public void SetRotation(Vector3 eulerAngle)
    {
        eleObj.transform.localRotation = Quaternion.Euler(eulerAngle);
    }
    public void SetEleObj(GameObject go)
    {
        eleObj = go;
    }
    public void SetVoltage(float v)
    {
        electryElement.Voltage = v;
    }
    public ElementType thisType {
        get {
            return elementType;
        }
    }
    public Vertex Pos {
        get {
            return pos;
        }set {
            pos = value;
        }
    }
    public Vertex Negative {
        get {
            return negative;
        }set {
            negative = value;
        }
    }
}
