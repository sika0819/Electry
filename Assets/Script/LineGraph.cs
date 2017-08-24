using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGraph{//电路图
    private Dictionary<int,Node> vertxList;//邻接表,每个顶点都要有。
    private int edgeCount=0;//边节点数量
    private List<List<Node>> circlelist;

    public LineGraph() {
        vertxList = new Dictionary<int, Node>();//储存序号
        circlelist = new List<List<Node>>();
    }
    public int VertexCount
    {//顶点数量
        get {
            return vertxList.Count;
        }
    }
    public int EdgeCount {
        get {
            return edgeCount;
        }
    }
    public void addVertex(Node vertex) {
        if (!vertxList.ContainsKey(vertex.index))
        {
            vertxList.Add(vertex.index,vertex);
        }
    }
    public void addEdge(Edge e) {
        Node v = e.either();
        Node w = e.other(v);
        vertxList[v.index].adj.Add(e);
        vertxList[w.index].adj.Add(e);
        edgeCount++;
    }
    public void addEdge(int v, int w) {
        Node a = vertxList[v];
        Node b = vertxList[w];
        Edge e = new Edge(a, b);
        addEdge(e);
    }
    public Node getVertex(int v) {
        return vertxList[v];
    }
    public List<Edge> getAdj(int v)
    {//返回v点相连的边
        return vertxList[v].adj;
    }
    public List<Edge> getEdges()
    {//返回所有便的集合
        List<Edge> edges = new List<Edge>();
        for (int i = 0; i < VertexCount; i++)
        {
            for (int e = 0; e < vertxList.Count; e++)
            {
                edges.Add(vertxList[i].adj[e]);
            }
        }
        return edges;
    }
    public string toString()
    {
        string s = VertexCount + " 个顶点, " + EdgeCount + " 条边\n";
        for (int i = 0; i < VertexCount; i++)
        {
            s += i + ": ";
            for (int j = 0; j < vertxList[i].adj.Count; j++)
            {
                Edge e = vertxList[i].adj[j];
                s += "节点："+e.other(vertxList[i]).index;
            }
            s += "\n";
        }
        return s;
    }

    public void removeEdge(Edge lineEdge)
    {
        for (int i = 0; i < VertexCount; i++)
        {
            for (int j = vertxList[i].adj.Count-1; j >=0; j--)
            {
                if (vertxList[i].adj[j].Equals(lineEdge))
                {
                    vertxList[i].adj.RemoveAt(j);
                }
            }
            
        }
    }
   
}
