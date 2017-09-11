using EasyAR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoARBind : MonoBehaviour {
    public EasyTargetBehavior[] targetBehaviorArray;
    ImageTrackerBehaviour tracker;
    // Use this for initialization
    void Awake () {
        tracker = FindObjectOfType<ImageTrackerBehaviour>(); ;
        targetBehaviorArray = FindObjectsOfType<EasyTargetBehavior>();
        for (int i = 0; i < targetBehaviorArray.Length; i++) {
            targetBehaviorArray[i].Bind(tracker);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
