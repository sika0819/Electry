using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleSwitch : Element {
    Animator switchAnim;
    public bool isTurnOn=false;

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
        CreateElement.Instance.ChanceSwitch(this, isTurnOn);
    }

}
