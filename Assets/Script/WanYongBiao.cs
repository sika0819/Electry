using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanYongBiao:Element {
    public float WanVoltage {
        get {
            if(LineEdge.Voltage!=0)
                NumberText.text = LineEdge.Voltage.ToString() + "V";
            return LineEdge.Voltage;
        }set {
            LineEdge.Voltage = value;
        }
    }
    public float WanCurrent {
        get {
            if (LineEdge2.Electry != 0)
                NumberText.text = LineEdge2.Electry.ToString() + "A";
            return LineEdge2.Electry;
        }set {
            LineEdge2.Electry = value;
        }
    }
    public Node VoltStartPoint {
        get {
            return startPoint;
        }set {
            startPoint = value;
        }
    }
    public Node VoltEndPoint {
        get {
            return endPoint;
        }set {
            endPoint = value;
        }
    }
    public Node ElecStartPoint;
    public Node ElecEndPoint;
    public GameObject StartElecNodeObj {
        get {
            return startElecObj;
        }
        set {
            startElecObj = value;
        }
    }
    private GameObject startElecObj;
    public GameObject EndElecNodeObj
    {
        get
        {
            return endElecObj;
        }
        set
        {
            endElecObj = value;
        }
    }
    private GameObject endElecObj;
    public Edge LineEdge2;
    public WanYongBiao(Element ele)
    {
        Init(ele);
    }
    TextMesh NumberText;

    public void InitVolmeter() {
        ElecStartPoint = new Node(GenrateIndex.Instance.Index);
        ElecEndPoint = new Node(GenrateIndex.Instance.Index);
        if (EleObj.transform.FindChild(ResourceTool.STARTPOINT+"(1)"))
        {
            startElecObj = EleObj.transform.FindChild(ResourceTool.STARTPOINT+"(1)").gameObject;
        }
        else
        {
            startElecObj = new GameObject();
            startElecObj.transform.SetParent(EleObj.transform);
        }
        startElecObj.name = ResourceTool.STARTPOINT + ElecStartPoint.index;
        if (EleObj.transform.FindChild(ResourceTool.ENDPOINT+"(1)"))
        {
            endElecObj = EleObj.transform.FindChild(ResourceTool.ENDPOINT + "(1)").gameObject;
        }
        else
        {
            endElecObj = new GameObject();
            endElecObj.transform.SetParent(EleObj.transform);
        }
        endElecObj.name = ResourceTool.STARTPOINT + ElecEndPoint.index;
        ElecStartPoint.InitGameObj(startElecObj);
        ElecEndPoint.InitGameObj(endElecObj);
        LineEdge2 = new Edge(ElecStartPoint,ElecEndPoint);
        LineEdge2.name = name;
        LineEdge.Resistance = 10000000f;
        LineEdge2.Resistance = 0;
        
        NumberText = EleObj.GetComponentInChildren<TextMesh>();
        WanVoltage = 0;
        WanCurrent = 0;
        NumberText.text = "0";
    }
    public void LinkVoltage(Edge e) {
        LineEdge = e;
        WanVoltage = e.Voltage;
    }
    public void LinkElectry(Edge e) {
        LineEdge2 = e;
        WanCurrent = e.Electry;
    }
    public override void Electry()
    {
        Debug.Log("读取电压：" + WanVoltage);
        Debug.Log("读取电流" + WanCurrent);
        if (WanVoltage != 0)
        {

        }
        else if (WanCurrent != 0)
        {

        }
        else
        {
            NumberText.text = "0";
        }
    }
}
