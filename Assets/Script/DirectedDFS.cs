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
    public DirectedDFS(DirectGraph G, int s)
    {
        marked = new bool[G.getVertexCount()];
        this.s = s;
        directEdgeTo = new int[G.getVertexCount()];
        dfs(G, s);
    }
    public DirectedDFS(LineGraph G,int s) {
        this.s = s;
        edgeTo = new int[G.VertexCount];
        directMarked = new bool[G.VertexCount];
        dfs(G, s);
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
    public bool hasPathTo(int v)
    {
        return marked[v];
    }
    public bool hasDirected(int v)
    {
        return directMarked[v];
    }
    public int getCount()
    {
        return count;
    }
    public Stack<int> pathTo(int v)
    {
        if (!hasPathTo(v)) return null;
        Stack<int> path = new Stack<int>();
        for (int x = v; x != s; x = edgeTo[x])
            path.Push(x);
        path.Push(s);
        return path;
    }
    public Stack<int> directedPathTo(int v) {
        if (!hasPathTo(v)) return null;
        Stack<int> path = new Stack<int>();
        for (int x = v; x != s; x = edgeTo[x])
            path.Push(x);
        path.Push(s);
        return path;
    }
}
