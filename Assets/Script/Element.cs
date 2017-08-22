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

    public GameObject startEleObj
    {
        get
        {
            return pos.PointObj;
        }
        set
        {
            pos.setGameObj(value);
        }
    }
    public GameObject endEleObj
    {
        get
        {
            return negative.PointObj;
        }
        set
        {
            negative.setGameObj(value);
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
        startEleObj = copyEle.startEleObj;
        endEleObj = copyEle.endEleObj;
    }
    public void Init(GameObject obj,int i)
    {
        
        eleObj = ResourceTool.InstitateGameObject(obj);
        eleObj.name = obj.name + i;
        
        pos = new Vertex(GenrateIndex.Instance.Index);
        negative = new Vertex(GenrateIndex.Instance.Index);
        electryElement = new ElecEdge(pos, negative);//从正极到负极建立一个有向边
        electryElement.Resistance = 0;
        electryElement.name = name;

        eleObj.transform.SetParent(consoleArea.transform);
        if (eleObj.transform.FindChild(ResourceTool.STARTPOINT))
        {
            startEleObj = eleObj.transform.FindChild(ResourceTool.STARTPOINT).gameObject;
        }
        else
        {
            startEleObj = new GameObject();
            startEleObj.transform.SetParent(eleObj.transform);
            startEleObj.name = ResourceTool.STARTPOINT;
        }
        if (obj.transform.FindChild(ResourceTool.ENDPOINT))
        {
            endEleObj = eleObj.transform.FindChild(ResourceTool.ENDPOINT).gameObject;
        }
        else
        {
            endEleObj = new GameObject();
            endEleObj.transform.SetParent(eleObj.transform);
            endEleObj.name = ResourceTool.ENDPOINT;
        }
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
    }
    public void SetPoint(Vector3 start, Vector3 end)
    {
        startEleObj.transform.localPosition = start;
        endEleObj.transform.localPosition = end;
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
