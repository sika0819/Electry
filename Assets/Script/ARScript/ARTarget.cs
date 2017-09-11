using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAR;
using System.Linq;

public class ARTarget : MonoBehaviour {
    private const string title = "Please enter KEY first!";
    private const string boxtitle = "===PLEASE ENTER YOUR KEY HERE===";
    private const string keyMessage = ""
        + "Steps to create the key for this sample:\n"
        + "  1. login www.easyar.com\n"
        + "  2. create app with\n"
        + "      Name: HelloARMultiTarget-SingleTracker (Unity)\n"
        + "      Bundle ID: cn.easyar.samples.unity.helloarmultitarget.st\n"
        + "  3. find the created item in the list and show key\n"
        + "  4. replace all text in TextArea with your key";

    private void Awake()
    {
        if (FindObjectOfType<EasyARBehaviour>().Key.Contains(boxtitle))
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog(title, keyMessage, "OK");
#endif
            Debug.LogError(title + " " + keyMessage);
        }
    }

    void CreateTarget(string targetName, out EasyTargetBehavior targetBehaviour)
    {
        GameObject Target = new GameObject(targetName);
        Target.transform.localPosition = Vector3.zero;
        targetBehaviour = Target.AddComponent<EasyTargetBehavior>();
    }

    void Start()
    {
        EasyTargetBehavior targetBehaviour;
        ImageTrackerBehaviour tracker = FindObjectOfType<ImageTrackerBehaviour>();
        
    }

}
