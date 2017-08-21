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
    static int createIndex=0;
   
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
        createIndex++;
        Debug.Log(obj.name);
        ele.Init(obj,createIndex);
        switch (createType) {
            case ElementType.Line:
                Rope rope = new Rope();
                rope.SetEleObj(ele.EleObj);
                ele = rope;
                rope.InitRope();
                break;
            case ElementType.Battery:
                Battery battery = new Battery();
                battery.SetEleObj(ele.EleObj);
                ele = battery;
                battery.InitBattery(10);
                break;
            case ElementType.Light:
                ElecLight elecLight = new ElecLight();
                elecLight.SetEleObj(ele.EleObj);
                ele = elecLight;
                elecLight.InitLight();
                break;
        }
        Ele = ele;
        elementList.Add(ele.name, ele);
    }
    public void UpdateElectry()
    {
        foreach (KeyValuePair<string, Element> item in elementList)
        {
        }
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
    public void OnDestory() {
        elementList = null;
        createIndex = 0;
    }
}
