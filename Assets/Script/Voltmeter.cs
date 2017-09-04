using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voltmeter:Element {
    public Node StartPoint {
        get {
            return startPoint;
        }set {
            startPoint = value;
        }
    }
    public Node EndPonit1 {
        get {
            return endPoint;
        }set {
            endPoint = value;
        }
    }
    public Node EndPoint2;
    public GameObject EndNodeObj2;
    public Edge LineEdge2;

    public Voltmeter(Element ele)
    {
        this.Init(ele);
    }

    public void InitVolmeter() {
        EndPoint2 = new Node(GenrateIndex.Instance.Index);
        if (EleObj.transform.FindChild(ResourceTool.ENDPOINT))
        {
            EndNodeObj2 = EleObj.transform.FindChild(ResourceTool.ENDPOINT).gameObject;
        }
        else
        {
            EndNodeObj2 = new GameObject();
            EndNodeObj2.transform.SetParent(EleObj.transform);
            EndNodeObj2.name = ResourceTool.ENDPOINT;
        }
        EndPoint2.InitGameObj(EndNodeObj2);
        LineEdge2 = new Edge(startPoint,EndPoint2);
    }
    public void setLineEdge1(Node point1, Node point2) {
        if (point1 != null && point2!=null){
            if (CreateElement.Instance.lineGraph.GetEdge(LineEdge) != null) {
                CreateElement.Instance.lineGraph.removeEdge(LineEdge);
            }
            LineEdge = new Edge(point1, point2);
            CreateElement.Instance.lineGraph.addEdge(LineEdge);
        }
    }
    public void setLineEdge2(Node point1, Node point2)
    {
        if (point1 != null && point2 != null)
        {
            if (CreateElement.Instance.lineGraph.GetEdge(LineEdge2) != null)
            {
                CreateElement.Instance.lineGraph.removeEdge(LineEdge2);
            }
            LineEdge2 = new Edge(point1, point2);
            CreateElement.Instance.lineGraph.addEdge(LineEdge2);
        }
    }
}
