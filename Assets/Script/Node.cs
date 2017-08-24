using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
   
    private int i;//序号
    public int index
    {
        get
        {
            return i;
        }
    }
    public List<Edge> adj;
    public Node() {
        adj = new List<Edge>();
    }
    public Node(int i)
    {
        this.i = i;
        adj = new List<Edge>();
    }
    public string name {
        get {
            return nodeObj.name;
        }
    }
    public GameObject NodeObj {
        get {
            return nodeObj;
        }
    }
    private GameObject nodeObj;
}
