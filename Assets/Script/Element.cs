using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element{//电子元器件基类
    private ElecEdge electryElement;//边节点
    private Vertex pos;//正极点
    private Vertex negative;//负极点
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

    public GameObject startVertexObj
    {
        get
        {
            if(pos!=null)
            return pos.PointObj;
            Debug.LogError("获取点程序错误");
            return null;
        }
        set
        {
            if (pos != null)
            {
                pos.setGameObj(value);
            }
            else {
                Debug.LogError("获取点程序错误");
            }
        }
    }
    public GameObject endVertexObj
    {
        get
        {
            if(negative!=null)
            return negative.PointObj;
            Debug.LogError("获取点程序错误");
            return null;
        }
        set
        {
            if (negative != null)
            {
                negative.setGameObj(value);
            }
            else {
                Debug.LogError("获取点程序错误");
            }
        }
    }
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
    public Element(float resistance)
    {
        pos = new Vertex(GenrateIndex.Instance.Index);
        negative = new Vertex(GenrateIndex.Instance.Index);
        electryElement = new ElecEdge(pos, negative);//从正极到负极建立一个有向边
        electryElement.Resistance = resistance;
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
            electryElement = new ElecEdge(pos, negative);//从正极到负极建立一个有向边
            electryElement.Resistance = 0;
            electryElement.name = name;
        }
        else {
            pos = new Vertex();
            negative = new Vertex();
        }
        
        if (eleObj.transform.FindChild(ResourceTool.STARTPOINT))
        {
            startVertexObj = eleObj.transform.FindChild(ResourceTool.STARTPOINT).gameObject;
        }
        else
        {
            startVertexObj = new GameObject();
            startVertexObj.transform.SetParent(eleObj.transform);
            startVertexObj.name = ResourceTool.STARTPOINT;
        }
        if (eleObj.transform.FindChild(ResourceTool.ENDPOINT))
        {
            endVertexObj = eleObj.transform.FindChild(ResourceTool.ENDPOINT).gameObject;
        }
        else
        {
            endVertexObj = new GameObject();
            endVertexObj.transform.SetParent(eleObj.transform);
            endVertexObj.name = ResourceTool.ENDPOINT;
        }
        if (initType != ElementType.Line)
        {
            startVertexObj.name = ResourceTool.STARTPOINT + pos.index;
            endVertexObj.name = ResourceTool.ENDPOINT + negative.index;
        }
    }
    public void SetPoint(Vector3 start, Vector3 end)
    {
        startVertexObj.transform.localPosition = start;
        endVertexObj.transform.localPosition = end;
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
