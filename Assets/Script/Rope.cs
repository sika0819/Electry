using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Element {
    public GameObject LinkObj1;
    public GameObject LinkObj2;
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
        if (!HasEdge&&node1!=null&&node2!=null)
        {
            LineEdge = new Edge(node1, node2);
        }
        else {
            CreateElement.Instance.lineGraph.removeEdge(LineEdge);
            LineEdge = new Edge(node1, node2);
            
        }
        LineEdge.name = name;
        CreateElement.Instance.lineGraph.addEdge(LineEdge);
    }
    public void RemoveLink(Node node1, Node node2)
    {
        if (!HasEdge && node1 != null && node2 != null)
        {
            CreateElement.Instance.lineGraph.removeEdge(LineEdge);
            LineEdge = null;
        }
    }
    
    public bool IsLinked{
        get {
            bool linked = Pos != null && Negative != null;
            return linked;
        }
    }
    public bool CanLink {
        get {
            return  LinkObj1!= null&&LinkObj2!=null;
        }
    }
    
}
