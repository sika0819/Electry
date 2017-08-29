using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public string parentName {
        get {
            return nodeObj.transform.parent.name;
        }
    }
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
    public void InitGameObj(GameObject obj) {
        nodeObj = obj;
    }
    public bool hasPathTo(int v)
    {
        foreach (Edge e in adj)
        {
            if ((e.either().index == v)||e.other(e.either()).index==v)
                return true;
        }
        return false;
    }
}
