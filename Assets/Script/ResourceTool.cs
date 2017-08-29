using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceTool{
    public const string STARTPOINT = "startPoint";
    public const string ENDPOINT = "endPoint";
    public const string POINT = "Point";
    public const string CONSOLEAREA = "ExperimentArea";
    public const string ENERGY = "energyBtn";
    public const string LINE = "lineBtn";
    public const string RESISTANCE = "resistanceBtn";
    public const string LIGHT = "lightBtn";
    public const string SWICTH = "switchBtn";
    public const string EXPCAMERA = "expCamera";
    public const string GROUND = "Ground";
    public const string PREFAB = "Prefab";
    public const string BTNTAG = "Button";
    public const string EXPAREA = "Area";
    public const string INFORM_TEXT = "InformText";
    public static string ROPE = "Rope";
    public static GameObject EnergyPreb;
    public static GameObject LinePreb;
    public static GameObject SwitchPreb;
    public static GameObject LightPreb;
    public static GameObject ResistancePreb;

   

    public static void InitResources() {
        EnergyPreb = Resources.Load<GameObject>("Prefab/Battery");
        LinePreb = Resources.Load<GameObject>("Prefab/Rope");
        SwitchPreb = Resources.Load<GameObject>("Prefab/Switch");
        ResistancePreb = Resources.Load<GameObject>("Prefab/Resistance");
        LightPreb = Resources.Load<GameObject>("Prefab/Light");
    }
    public static GameObject InstitateGameObject(GameObject prefab) {
        //Debug.Log(prefab.name);
        GameObject temp;
        temp= GameObject.Instantiate<GameObject>(prefab);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localRotation = Quaternion.identity;
        temp.transform.localScale = Vector3.one;
        temp.name = prefab.name;
        return temp;
   }
    
}
