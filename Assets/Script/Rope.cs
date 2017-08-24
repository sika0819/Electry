using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Element {
    public Node linkNode {
        get {
            return _linkEle;
        }set {
            if (_linkEle == null)
            {
                _linkEle = value;
            }
            else
            {
                Link(_linkEle, value);
                _linkEle = value;
            }
                
        }
    }
    private Node _linkEle;
    public UltimateRope ropeControl;
    bool HasEdge {
        get {
            return LineEdge != null;
        }
    }
    int i = 0;
    public Rope() {

    }
    public Rope(Element e) {

        this.Init(e);
    }
    public void InitRope() {
        if (EleObj != null)
        {
            ropeControl = EleObj.GetComponentInChildren<UltimateRope>();
            if (ropeControl == null) {
                Debug.LogError("创建绳子错误");
            }
            ropeControl.Regenerate(true);
        }
    }
    public void SetLength(int length )
    {
        ropeControl.TotalRopeLength = length;
    }
    
    public void Link(Node node1, Node node2) {
        if (!HasEdge)
        {
            LineEdge = new Edge(node1, node2);
            CreateElement.Instance.lineGraph.addEdge(LineEdge);
        }
        else {
            CreateElement.Instance.lineGraph.removeEdge(LineEdge);
            LineEdge = new Edge(node1, node2);
            CreateElement.Instance.lineGraph.addEdge(LineEdge);
        }
    }
    public bool IsLinked{
        get {
            bool linked = Pos != null && Negative != null;
            if (linked)
                ElectryEdge = new ElecEdge(Pos,Negative);
            return linked;
        }
    }
   
}
