using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Element {
    public GameObject[] linkArray;
    public UltimateRope ropeControl;
    int i = 0;
    public Rope() {
        linkArray = new GameObject[2];
    }
    public Rope(Element e) {
        linkArray = new GameObject[2];
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
    
    public void Link(Vertex pos, Vertex negative) {
        this.Pos = pos;
        this.Negative = negative;
        ElectryEdge = new ElecEdge(Pos,Negative);
    }
    public bool IsLinked{
        get {
            return Pos != null && Negative != null;
        }
    }
    public void Link() {
       
        if (linkArray[0] != null && linkArray[1] != null)
        {
            Vertex v1= CreateElement.Instance.g.getVertex(linkArray[0].name);
            
            if (IsLinked)
                Link(Pos, Negative);
        }
    }
}
