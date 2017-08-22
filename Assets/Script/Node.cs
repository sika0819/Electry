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
    public Node(int i)
    {
        this.i = i;
        adj = new List<Edge>();
    }

}
