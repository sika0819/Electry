using EasyAR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyTargetBehavior : ImageTargetBehaviour
{
    string targetName="";
    protected override void Awake()
    {
        base.Awake();
        TargetFound += OnTargetFound;
        TargetLost += OnTargetLost;
        TargetLoad += OnTargetLoad;
        TargetUnload += OnTargetUnload;
        
    }

    void OnTargetFound(TargetAbstractBehaviour behaviour)
    {
        targetName= Target.Name.Replace(ResourceTool.IMAGE_TARGET, "");
        switch (targetName) {
            case ResourceTool.LINEBTN:
                
                break;
        }
    }

    void OnTargetLost(TargetAbstractBehaviour behaviour)
    {
        Debug.Log("Target Lost: " + Target.Id + " (" + Target.Name + ") ");
    }

    void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {
        Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
    }

    void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {
        Debug.Log("Unload target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
    }
}
