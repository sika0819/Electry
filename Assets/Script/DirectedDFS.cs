using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedDFS
{
    private bool[] marked;  // marked[v] = true if v is reachable
                            // from source (or sources)
    private bool[] directMarked;
    private int count;         // number of vertices reachable from s
    private int[] edgeTo;        // edgeTo[v] = last edge on s-v path
    private int[] directEdgeTo;
    private int s;         // source vertex
    public DirectedDFS(DirectGraph G, Vertex s)
    {
        directMarked = new bool[G.getVertexCount()];
        this.s = s.index;
        directEdgeTo = new int[G.getVertexCount()];
        dfs(G, s.index);
    }
    public DirectedDFS(LineGraph G,Node s) {
        this.s = s.index;
        edgeTo = new int[G.VertexCount];
        marked = new bool[G.VertexCount];
        dfs(G, s.index);
    }
    private void dfs(LineGraph G, int v)
    {
        marked[v] = true;
        for (int w=0;w< G.getAdj(v).Count;w++)
        {
            if (!marked[w])
            {
                edgeTo[w] = v;
                dfs(G, w);
            }
        }
    }
    public DirectedDFS(DirectGraph G, List<int> sources)
    {
        directMarked = new bool[G.getVertexCount()];
        for (int v=0;v< sources.Count;v++)
        {
            if (!directMarked[v]) dfs(G, v);
        }
    }
    private void dfs(DirectGraph G, int v)
    {
        count++;
        directMarked[v] = true;
        for (int w=0;w< G.getAdj(v).Count;w++)
        {
            if (!directMarked[w]) {
                directEdgeTo[w] = v;
                dfs(G, w);
            }
        }
    }
    public bool hasPathTo(Node v)
    {
        return marked[v.index];
    }
    public bool hasDirected(Vertex v)
    {
        return directMarked[v.index];
    }
    public int getCount()
    {
        return count;
    }
    public List<int> pathTo(Node v)
    {
        if (!hasPathTo(v)) return null;
        List<int> path = new List<int>();
        for (int x = v.index; x != s; x = edgeTo[x])
            path.Add(x);
        path.Add(s);
        return path;
    }
    public List<int> directedPathTo(Vertex v) {
        if (!hasDirected(v)) return null;
        List<int> path = new List<int>();
        for (int x = v.index; x != s; x = directEdgeTo[x])
            path.Add(x);
        path.Add(s);
        return path;
    }
}
