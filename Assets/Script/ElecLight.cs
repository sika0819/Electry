using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecLight : Element {
    Light elecLight;
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
        InitEleType(ElementType.Light);
        elecLight = EleObj.GetComponentInChildren<Light>();
        TurnLight(false);
        SetResistance(5);
    }
    void JudgeLight() {
        if (isTurnOn)
        {
            TurnLight(true);
        }
        else {
            TurnLight(false);
        }
    }
    public void TurnLight(bool isOn) {
        elecLight.enabled = isOn;
       
    }
    public override void Electry()
    {
        base.Electry();
        JudgeLight();
    }
}
