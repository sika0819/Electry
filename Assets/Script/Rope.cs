using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Element {
    public UltimateRope ropeControl;
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
}
