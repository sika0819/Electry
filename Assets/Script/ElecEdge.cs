using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecEdge {//电流图的边
    public string name;
    private float resistance;//电阻
    private float voltage;//电压
    private float electry;//电流
    private Vertex from;
    private Vertex to;

    public ElecEdge(Vertex from, Vertex to)
    {
        this.from = from;
        this.to = to;
    }
    
    public Vertex getFrom()
    {
        return from;
    }

    public Vertex getTo()
    {
        return to;
    }

    public float Resistance
    {
        get
        {
            return resistance;
        }
        set
        {
            resistance = value;
        }
    }
    public float Voltage
    {
        get
        {
            return voltage;
        }
        set
        {
            voltage = value;
        }
    }

    public float Electry {
        get {
            return electry;
        }
        set {
            electry = value;
        }
    }

    public override string ToString()
    {
        string s = from.index + " -> " + to.index + ", 电阻: " + resistance+"电压："+voltage+"电流："+electry+";    ";
        return s;
    }
    public bool hasPathTo(int v) {
        if (to.index == v)
            return true;
        return false;
    }
}
