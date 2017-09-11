using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecLight : Element {
    Light elecLight;
    public ElecLight(Element e) {
        Init(e);
    }
    public bool isTurnOn {
        get {
            return turnOn;
        }set {
            turnOn = value;
            TurnLight(value);
        }
    }
    private bool turnOn=false;
    public void InitLight() {
        elecLight = EleObj.GetComponentInChildren<Light>();
        TurnLight(false);
        SetResistance(5);
    }
    
    public void TurnLight(bool isOn) {
        if(elecLight!=null)
        elecLight.enabled = isOn;
       
    }
    public override void Electry()
    {
        //Debug.Log("电灯泡"+name+"当前电流为："+Currency);
        if (Currency > 0)
        {
            //  CreateElement.Instance.ShowInform = "灯泡连接成功，发光中";
            isTurnOn = true;
        }
        else {
            isTurnOn = false;
        }
    }
}
