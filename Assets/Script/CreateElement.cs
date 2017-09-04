using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ElementType
{
    Battery,
    Line,
    Switch,
    Resistance,
    Light,
    Voltmeter,
    Default
}
public class CreateElement {
    public static Dictionary<string, Element> elementList=new Dictionary<string, Element>();
    Battery onlyBattery;
    List<KeyValuePair<string, Element>> resistanceList;
    List<KeyValuePair<string, ElecLight>> lightList;
    List<KeyValuePair<string, EleSwitch>> swicthList;
    List<KeyValuePair<string, Rope>> ropeList;
    static int createIndex=0;
    Text InformText;
   
    public LineGraph lineGraph;
    public CreateElement() {
        ropeList = new List<KeyValuePair<string, Rope>>();
        resistanceList = new List<KeyValuePair<string, Element>>();
        lightList = new List<KeyValuePair<string, ElecLight>>();
        swicthList = new List<KeyValuePair<string, EleSwitch>>();
        lineGraph = new LineGraph();
    }
    public static CreateElement Instance {
        get
        {
            if (_instance == null)
            {
                _instance = new CreateElement();
            }
            return _instance;
        }
    }
    public GameObject elePreb;
    public Element Ele;
    public string ShowInform
    {
        get
        {
            if (InformText != null)
                return InformText.text;
            else
                throw new Exception("信息显示框为空");
        }
        set
        {
            if(InformText!=null&&InformText.text!=value)
                InformText.text ="提示信息："+ value;
        }
    }
    private static CreateElement _instance;
    
    public void Init(GameObject obj,ElementType createType)
    {
        elePreb = obj;
        Element ele = new Element();
        createIndex++;
        if (createType != ElementType.Battery)
        {
            ele.Init(elePreb, createIndex);
            ele.InitEleType(createType);
        }
        switch (createType) {
            case ElementType.Line:
                Rope rope = new Rope(ele);
                rope.InitRope();  
                ele = rope;
                ropeList.Add(new KeyValuePair<string, Rope>(rope.name, rope));
                break;
            case ElementType.Battery:
                if (onlyBattery == null)
                {
                    ele.Init(elePreb, createIndex);
                    ele.InitEleType(createType);
                    Battery battery = new Battery(ele);
                    battery.InitBattery(10);
                    lineGraph.OnlyBattery = battery;
                    ele = battery;
                    ele.Voltage = 10;
                    onlyBattery = battery;
                }
                else {
                    ele = null;
                    ShowInform = "只允许有一个电池！";
                }
                break;
            case ElementType.Light:
                ele.SetResistance(5);
                ElecLight elecLight = new ElecLight(ele);
                elecLight.InitLight();
                elecLight.SetResistance(5);
                ele = elecLight;
                lightList.Add(new KeyValuePair<string, ElecLight>(elecLight.name, elecLight));
                break;
            case ElementType.Switch:
                EleSwitch eleSwitch = new EleSwitch(ele);
                eleSwitch.InitSwitch();
                ele = eleSwitch;
                eleSwitch.SetEleObj(eleSwitch.EleObj);
                swicthList.Add(new KeyValuePair<string, EleSwitch>(eleSwitch.name,eleSwitch));
                break;
            case ElementType.Resistance:
                ele.SetResistance(5);
                resistanceList.Add(new KeyValuePair<string, Element>(ele.name, ele));
                break;
            case ElementType.Voltmeter:
                Voltmeter eleVoltemeter = new Voltmeter(ele);
                eleVoltemeter.InitVolmeter();
                break;
        }
        if (createType != ElementType.Line&&ele!=null)
        {
            lineGraph.addVertex(ele.startPoint);
            lineGraph.addVertex(ele.endPoint);
            lineGraph.addEdge(ele.LineEdge);
        }
        Ele = ele;
        if(ele!=null)
        elementList.Add(ele.name, ele);

    }

    public void SetInformBox(Text informBox)
    {
        this.InformText = informBox;
    }

    public void Update(){//每帧更新
        
        
    }
  
    public Node GetPoint(string name)
    {
        foreach(KeyValuePair<string,Element>item in elementList) {
            if (item.Value.startPoint.name == name)
            {
                return item.Value.startPoint;
            }
            if (item.Value.endPoint.name == name)
            {
                return item.Value.endPoint;
            }
        }
        return null;
    }
    
    public void SetPoint(Vector3 startPoint,Vector3 endPoint)
    {
        if (Ele != null)
        {
            Ele.SetPoint(startPoint, endPoint);
        }
    }
    public Element GetElement(string name) {
        if(elementList.ContainsKey(name))
        return elementList[name];
        return null;
    }
    public Element GetResistance(string name) {
        for (int i = 0; i < resistanceList.Count; i++) {
            if (resistanceList[i].Key == name)
                return resistanceList[i].Value;
        }
        return null;
    }
    public ElecLight GetEleLight(string name)
    {
        for (int i = 0; i < lightList.Count; i++)
        {
            if (lightList[i].Key == name)
                return lightList[i].Value;
        }
        return null;
    }
    public Battery GetBattery()
    {
        return onlyBattery;
    }
    public EleSwitch GetSwitch(string name)
    {
        for (int i = 0; i < swicthList.Count; i++)
        {
            Debug.Log(swicthList[i].Key);
            if (swicthList[i].Key == name)
                return swicthList[i].Value;
        }
        return null;
    }
    public Rope GetRope(string name)
    {
        for (int i = 0; i < ropeList.Count; i++)
        {
            if (ropeList[i].Key == name)
                return ropeList[i].Value;
        }
        return null;
    }
    

    public void OnDestory() {
        elementList.Clear();
        elementList = null;
        createIndex = 0;
        swicthList.Clear();
        onlyBattery = null;
        ropeList.Clear();
    }
}
