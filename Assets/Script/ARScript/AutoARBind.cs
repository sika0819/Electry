using EasyAR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AutoARBind : MonoBehaviour {
    public EasyTargetBehavior[] targetBehaviorArray;
    ImageTrackerBehaviour tracker;
    GameObject ExperimentArea;
    bool isShowDesk = false;
    // Use this for initialization
    void Awake () {
        ExperimentArea = GameObject.Find(ResourceTool.CONSOLEAREA);
        tracker = FindObjectOfType<ImageTrackerBehaviour>(); 
        targetBehaviorArray = FindObjectsOfType<EasyTargetBehavior>();
        for (int i = 0; i < targetBehaviorArray.Length; i++) {
            targetBehaviorArray[i].Bind(tracker);
        }
	}

    // Update is called once per frame
    void Update () {

	}
}
