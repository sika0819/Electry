using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Element {
    public float Voltage;//电压
    public void InitBattery(float v) {
        SetVoltage(v);
        SetResistance(0);//因为很麻烦所以电池的电阻默认为0
        
    }

    private void SetVoltage(float v)
    {
        Voltage = v;
    }

    

}
