using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle
{
    private bool[] marked;
    private int[] edgeTo;
    private Stack<int> cycle;
    public Circle(LineGraph G)
    {
        if (hasSelfLoop(G)) return;
        if (hasParallelEdges(G)) return;
        marked = new bool[G.VertexCount];
        edgeTo = new int[G.VertexCount];
        for (int v = 0; v < G.VertexCount; v++)
            if (!marked[v])
                dfs(G, -1, v);
    }
    // does this graph have a self loop?
    // side effect: initialize cycle to be self loop
    private bool hasSelfLoop(LineGraph G)
    {
        for (int v = 0; v < G.VertexCount; v++)
        {
            for (int w=0;w< G.getAdj(v).Count;w++)
            {
                if (v == w)
                {
                    cycle = new Stack<int>();
                    cycle.Push(v);
                    cycle.Push(v);
                    return true;
                }
            }
        }
        return false;
    }
    // does this graph have two parallel edges?
    // side effect: initialize cycle to be two parallel edges
    private bool hasParallelEdges(LineGraph G)
    {
        marked = new bool[G.VertexCount];
        for (int v = 0; v < G.VertexCount; v++)
        {

            // check for parallel edges incident to v
            for (int w=0;w< G.getAdj(v).Count;w++)
            {
                if (marked[w])
                {
                    cycle = new Stack<int>();
                    cycle.Push(v);
                    cycle.Push(w);
                    cycle.Push(v);
                    return true;
                }
                marked[w] = true;
            }

            // reset so marked[v] = false for all v
            for (int w=0;w< G.getAdj(v).Count;w++)
            {
                marked[w] = false;
            }
        }
        return false;
    }
    public bool hasCycle()
    {
        return cycle != null;
    }
    public Stack<int> getCycle()
    {
        return cycle;
    }
    private void dfs(LineGraph G, int u, int v)
    {
        marked[v] = true;
        for (int w=0;w< G.getAdj(v).Count;w++)
        {

            // short circuit if cycle already found
            if (cycle != null) return;

            if (!marked[w])
            {
                edgeTo[w] = v;
                dfs(G, v, w);
            }
            // check for cycle (but disregard reverse of edge leading to v)
            else if (w != u)
            {
                cycle = new Stack<int>();
                for (int x = v; x != w; x = edgeTo[x])
                {
                    cycle.Push(x);
                }
                cycle.Push(w);
                cycle.Push(v);
            }
        }
    }
    public override string ToString()
    {
        string result="";
        for (int i = 0; i < cycle.Count; i++) {
            result += cycle.Pop()+"————>";
        }
        return result;
    }
}
