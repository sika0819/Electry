using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleSwitch : Element {
    Animator switchAnim;
    bool isTurnOn=false;

    public EleSwitch(Element ele)
    {
        Init(ele);
    }

    public void InitSwitch() {
        SetResistance(0);
        switchAnim = EleObj.GetComponentInChildren<Animator>();
    }
    public void ToggleTurn() {//点击的时候切换
        isTurnOn = !isTurnOn;
        switchAnim.SetBool("isOn",isTurnOn);
    }
    public void isTurn() {
        //当开关为开时，遍历，如果和电池联通，则电流等于电池的电流。关的时候电流等于0
        if (isTurnOn)
        {

        }
        else {
            setCurrency(0);
        }
    }
}
