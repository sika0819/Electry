using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElementTest{
    public ElementType elementType {
        get {
            return _eleType;
        }
    }
    public ElementType _eleType;
    public float Voltage {
        get {
            return voltage;
        }
    }//电压
    public float Resistance {
        get {
            return resistance;
        }
    }//电阻
    public float Current {
        get {
            return current;
        }
    }//电流
    public bool isVisited;//访问过一次
    public bool startIsLinked=false;//点1正被电路连接
    public bool endIsLinked = false;//点2正被电路连接
    private GameObject startPoint;//连接电线用的点
    public Element startEle;//点1连接的点
    private GameObject endPoint;//连接电线用的点
    public Element endEle;//点2连接的点。
    private GameObject consoleArea;//父物体
    private GameObject eleObj;
    private float voltage;
    private float resistance;
    private float current;
    public GameObject EleObj
    {
        get
        {
            return eleObj;
        }
    }
    public Vector3 Pos
    {
        get
        {
            return eleObj.transform.localPosition;
        }
    }
    public void Init(GameObject obj,string name) {
        consoleArea = GameObject.Find(ResourceTool.CONSOLEAREA);
        eleObj =  ResourceTool.InstitateGameObject(obj);
        eleObj.name = name;
        eleObj.transform.SetParent(consoleArea.transform);
        if (obj.transform.FindChild(ResourceTool.STARTPOINT))
        {
            startPoint = obj.transform.FindChild(ResourceTool.STARTPOINT).gameObject;
        }
        else {
            startPoint = new GameObject();
            startPoint.transform.SetParent(obj.transform);
            startPoint.name = ResourceTool.STARTPOINT;
        }
        if (obj.transform.FindChild(ResourceTool.ENDPOINT))
        {
            endPoint = obj.transform.FindChild(ResourceTool.ENDPOINT).gameObject;
        }
        else {
            endPoint = new GameObject();
            endPoint.transform.SetParent(obj.transform);
            endPoint.name = ResourceTool.ENDPOINT;
        }
    }
    public void InitEleType(ElementType type) {
        _eleType = type;
    }
    public void SetPoint(Vector3 start,Vector3 end) {
        startPoint.transform.localPosition = start;
        endPoint.transform.localPosition = end;
    }
    public void SetPosition(Vector3 pos) {
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
    public void SetVoltage(float v) {
        voltage = v;
    }
    public void SetResistance(float r) {
        resistance = r;
    }
    public void SetCurrent(float c) {
        current = c;
    }
    public void LinkStart(Element linkPoint) {
        startIsLinked = true;
        startEle = linkPoint;
    }
    public virtual void Electry() {
        if (elementType != ElementType.Battery)
        {
            if (startIsLinked)
            {
                //发电，放在Update函数里进行。遍历连接点，进行发电
                SetCurrent(startEle.Currency);
            }
            if (endIsLinked)
            {
                SetCurrent(endEle.Currency);
            }
        }
        else {
            if (startIsLinked)
            {
                //发电，放在Update函数里进行。遍历连接点，进行发电
              
            }
            if (endIsLinked)
            {
                
            }
        }
    }
}
