using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ElementType
{
    Battery,
    Line,
    Switch,
    Resistance,
    Light,
    Default
}
public class CreateElement {
    public static Dictionary<string, Element> elementList=new Dictionary<string, Element>();
    List<KeyValuePair<string,Battery>> batteryList;
    List<KeyValuePair<string, Element>> resistanceList;
    List<KeyValuePair<string, ElecLight>> lightList;
    List<KeyValuePair<string, EleSwitch>> swicthList;
    List<KeyValuePair<string, Rope>> ropeList;
    static int createIndex=0;
    public DirectGraph g;
    public CreateElement() {
        batteryList = new List<KeyValuePair<string, Battery>>();
        ropeList = new List<KeyValuePair<string, Rope>>();
        resistanceList = new List<KeyValuePair<string, Element>>();
        lightList = new List<KeyValuePair<string, ElecLight>>();
        swicthList = new List<KeyValuePair<string, EleSwitch>>();
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
    private static CreateElement _instance;

    public void Init(GameObject obj,ElementType createType)
    {
        elePreb = obj;
        Element ele = new Element();
        g = new DirectGraph();
        createIndex++;

        ele.Init(obj,createIndex);
        ele.InitEleType(createType);
       
        switch (createType) {
            case ElementType.Line:
                Rope rope = new Rope(ele);
                rope.InitRope();
                ropeList.Add(new KeyValuePair<string, Rope>(rope.name, rope));
                break;
            case ElementType.Battery:
                Battery battery = new Battery(ele);
                battery.InitBattery(10);
                batteryList.Add(new KeyValuePair<string, Battery>(battery.name,battery));
                ele = battery;
                break;
            case ElementType.Light:
                ElecLight elecLight = new ElecLight(ele);
                ele = elecLight;
                lightList.Add(new KeyValuePair<string, ElecLight>(elecLight.name, elecLight));
                elecLight.InitLight();
                break;
            case ElementType.Switch:
                EleSwitch eleSwitch = new EleSwitch(ele);
                ele = eleSwitch;
                eleSwitch.SetEleObj(eleSwitch.EleObj);
                break;
            case ElementType.Resistance:
                ele.SetResistance(5);
                break;
        }
        g.addVertex(ele.Pos);
        g.addVertex(ele.Negative);
        g.addEdge(ele.ElectryEdge);
        Ele = ele;
        elementList.Add(ele.name, ele);
    }
    public void UpdateElectry()
    {
        
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
    public Battery GetBattery(string name)
    {
        for (int i = 0; i < batteryList.Count; i++)
        {
            if (batteryList[i].Key == name)
                return batteryList[i].Value;
        }
        return null;
    }
    public EleSwitch GetSwitch(string name)
    {
        for (int i = 0; i < swicthList.Count; i++)
        {
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
        batteryList.Clear();
        ropeList.Clear();
    }
}
