using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Element {
 
    public UltimateRope ropeControl;
    public Rope() { }
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
    public void Link(Vertex pos, Vertex negative) {
        this.Pos = pos;
        this.Negative = negative;
        ElectryEdge = new ElecEdge(Pos,Negative);
    }
}
