using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge{//电子元器件相当于加权边，有两个节点
    private Node v;//其中一个节点
    private Node w;//另一个节点
    private float resistance;//电阻
    private float voltage;//电压
    public string name;

    public Edge(Node v, Node w)
    {
        this.v = v;
        this.w = w;
    }

    public Node either()
    { // 返回其中一个节点  
        return v;
    }

    public Node other(Node i)
    { // 已知一个节点，返回另一个节点  
        if (i.index == v.index) return w;
        if (i.index == w.index) return v;
        Debug.Log("error! arg expect: " + v + " or " + w + ",but receive:" + i);
        return null;
    }
    public void SetNode(Node i,Node j) {
        v = i;
        w = j;
    }

    public override string ToString()
    {
        string s = v.index + " to " + w.index ;
        return s;
    }
    public float Resistance {
        get {
            return resistance;
        }set {
            resistance = value;
        }
    }
    public float Voltage {
        get {
            return voltage;
        }set {
            voltage = value;
        }
    }
    float currency = 0;
    public float Electry {
        get {
            return currency;
        }set {
            currency = value;
        }
    }
    public bool isEqual(Edge e) {
        bool equal = false;
        if (e.either().index==v.index&&e.other(v).index==w.index)
        {
            equal = true;
        }
        return equal;
    }
}
