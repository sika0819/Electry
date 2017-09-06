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
    List<KeyValuePair<string, WanYongBiao>> voltmeterList;
    static int createIndex=0;
    Text InformText;
   
    public LineGraph lineGraph;
    public CreateElement() {
        ropeList = new List<KeyValuePair<string, Rope>>();
        resistanceList = new List<KeyValuePair<string, Element>>();
        lightList = new List<KeyValuePair<string, ElecLight>>();
        swicthList = new List<KeyValuePair<string, EleSwitch>>();
        voltmeterList = new List<KeyValuePair<string, WanYongBiao>>();
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
                swicthList.Add(new KeyValuePair<string, EleSwitch>(eleSwitch.name,eleSwitch));
                break;
            case ElementType.Resistance:
                ele.SetResistance(5);
                resistanceList.Add(new KeyValuePair<string, Element>(ele.name, ele));
                break;
            case ElementType.Voltmeter:
                WanYongBiao eleVoltemeter = new WanYongBiao(ele);
                eleVoltemeter.InitVolmeter();
                ele = eleVoltemeter;
                
                voltmeterList.Add(new KeyValuePair<string, WanYongBiao>(eleVoltemeter.name, eleVoltemeter));
                break;
        }
        if (createType != ElementType.Line && ele != null && createType != ElementType.Voltmeter)
        {
            lineGraph.addVertex(ele.startPoint);
            lineGraph.addVertex(ele.endPoint);
            lineGraph.addEdge(ele.LineEdge);
        }
        else if (createType == ElementType.Voltmeter)
        {
            lineGraph.addVertex(GetVoltmeter(ele.name).VoltStartPoint);
            lineGraph.addVertex(GetVoltmeter(ele.name).VoltEndPoint);
            lineGraph.addVertex(GetVoltmeter(ele.name).ElecStartPoint);
            lineGraph.addVertex(GetVoltmeter(ele.name).ElecEndPoint);
            lineGraph.addEdge(GetVoltmeter(ele.name).LineEdge);
            lineGraph.addEdge(GetVoltmeter(ele.name).LineEdge2);
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
        foreach (KeyValuePair<string,WanYongBiao>item in voltmeterList) {
           // item.Value.ReadValue();
        }
    }
  
    public Node GetPoint(string name)
    {
        return lineGraph.GetNode(name);
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
        Debug.Log(name);
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
            //Debug.Log(swicthList[i].Key);
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

    public WanYongBiao GetVoltmeter(string name) {
        for (int i = 0; i < voltmeterList.Count; i++)
        {
            if (voltmeterList[i].Key == name)
                return voltmeterList[i].Value;
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
