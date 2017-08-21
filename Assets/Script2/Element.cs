using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element{//电子元器件基类
    private ElecEdge electryElement;//边节点
    private Vertex pos;//正极点
    private Vertex negative;//负极点
    private ElementType elementType;
    public float Currency {
        get {
            return pos.Electry;
        }
    }
    public float Resistance {
        get {
            return electryElement.Resistance;
        }set {
            electryElement.Resistance = value;
        }
    }
    public GameObject EleObj//电子元器件物体
    {
        get {
            return eleObj;
        }set {
            eleObj = value;
        }
    }
    private GameObject eleObj;
    private GameObject consoleArea;
    public string name {
        get {
            return eleObj.name;
        }
    }

    public void Init(GameObject obj,int i)
    {
        eleObj = obj;
        eleObj.name = obj.name+i;
        Debug.Log(obj.name);
        Debug.Log(eleObj.name);
        consoleArea = GameObject.Find(ResourceTool.CONSOLEAREA);
        eleObj = ResourceTool.InstitateGameObject(obj);
        eleObj.transform.SetParent(consoleArea.transform);
        if (obj.transform.FindChild(ResourceTool.STARTPOINT))
        {
            startEleObj = obj.transform.FindChild(ResourceTool.STARTPOINT).gameObject;
        }
        else
        {
            startEleObj = new GameObject();
            startEleObj.transform.SetParent(obj.transform);
            startEleObj.name = ResourceTool.STARTPOINT;
        }
        if (obj.transform.FindChild(ResourceTool.ENDPOINT))
        {
            endEleObj = obj.transform.FindChild(ResourceTool.ENDPOINT).gameObject;
        }
        else
        {
            endEleObj = new GameObject();
            endEleObj.transform.SetParent(obj.transform);
            endEleObj.name = ResourceTool.ENDPOINT;
        }
    }
    public Element() {
        pos = new Vertex(GenrateIndex.Instance.Index);
        negative = new Vertex(GenrateIndex.Instance.Index);
        electryElement = new ElecEdge(pos, negative);//从正极到负极建立一个有向边
        electryElement.Resistance = 0;
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
    public ElecEdge ElectryElement{//返回元器件
        get {
            return electryElement;
        }
    }

    public GameObject startEleObj {
        get {
            return pos.PointObj;
        }
        set {
            pos.setGameObj(value);
        }
    }
    public GameObject endEleObj {
        get {
            return negative.PointObj;
        }set {
            negative.setGameObj(value);
        }
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
}
