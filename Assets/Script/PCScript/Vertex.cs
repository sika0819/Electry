﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex {
   
    private int i;//序号
    public int index {
        get {
            return i;
        }
    }
    public float Electry {
        get {
            return electry;
        }
    }
    private float electry=0;
    public List<ElecEdge> adj;
    private GameObject pointObj;
    public Vertex(int i) {
        this.i = i;
        adj = new List<ElecEdge>();
    }

    public Vertex()
    {
        
    }

   
    public void setElectry(float e) {
        electry = e;//当前点电流
    }
    public ElecEdge head {
        get {
            if (adj.Count > 0)
                return adj[0];
            else
                return null;
        }
    }
    public bool hasPathTo(int v) {
        foreach (ElecEdge e in adj) {
            if (e.getTo().index == v)
                return true;
        }
        return false;
    }
}
