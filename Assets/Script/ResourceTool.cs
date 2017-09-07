using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceTool{
    public const string STARTPOINT = "startPoint";
    public const string ENDPOINT = "endPoint";
    public const string POINT = "Point";
    public const string CONSOLEAREA = "ExperimentArea";
    public const string EXPERIMENT = "Experiment";
    public const string ENERGYBTN = "energyBtn";
    public const string LINEBTN = "lineBtn";
    public const string RESISTANCEBTN = "resistanceBtn";
    public const string LIGHTBTN = "lightBtn";
    public const string VOLTMETERBTN = "wanYongBiaoBtn";
    public const string SWICTHBTN = "switchBtn";
    public const string EXPCAMERA = "expCamera";
    public const string GROUND = "Ground";
    public const string PREFAB = "Prefab";
    public const string BTNTAG = "Button";
    public const string EXPAREA = "Area";
    public const string INFORM_TEXT = "InformText";
    public static string ROPE = "Rope";
    public static string ROPENODE = "RopeNode";
    public static string WANYONGBIAO = "WanYongBiao";
    public static string DIALOGBOX = "DialogBox";
    public static string SURE_BTN = "SureBtn";
    public static string CANCEL_BTN = "CancelBtn";
    public static GameObject EnergyPreb;
    public static GameObject LinePreb;
    public static GameObject SwitchPreb;
    public static GameObject LightPreb;
    public static GameObject ResistancePreb;
    public static GameObject WanYongBiaoPreb;

    public static void InitResources() {
        EnergyPreb = Resources.Load<GameObject>("Prefab/Battery");
        LinePreb = Resources.Load<GameObject>("Prefab/Rope");
        SwitchPreb = Resources.Load<GameObject>("Prefab/Switch");
        ResistancePreb = Resources.Load<GameObject>("Prefab/Resistance");
        LightPreb = Resources.Load<GameObject>("Prefab/Light");
        WanYongBiaoPreb = Resources.Load<GameObject>("Prefab/WanYongBiao");
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
    public static void DestoryGameObject(GameObject go) {
        if(go!=null)
        GameObject.Destroy(go);
    }
    public static Vector3 WorldToUIPoint(Camera expCamera, Canvas canvas, Vector3 pos)
    {
        Vector3 v_v3 = expCamera.WorldToScreenPoint(pos);
        Vector3 v_ui = canvas.worldCamera.ScreenToWorldPoint(v_v3);
        Vector3 v_new = new Vector3(v_ui.x, v_ui.y, canvas.GetComponent<RectTransform>().anchoredPosition3D.z);
        return v_new;
    }
}
