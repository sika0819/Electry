using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGraph{//电路图
    private Dictionary<int,Node> vertxList;//邻接表,每个顶点都要有。
    private int edgeCount=0;//边节点数量
    private List<List<Node>> circlelist;
    public Element battery;
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
        //Debug.Log("添加节点："+vertex.index);
        if (!vertxList.ContainsKey(vertex.index))
        {
            vertxList.Add(vertex.index,vertex);
        }
    }
    public void addEdge(Edge e) {
        Node v = e.either();
        Node w = e.other(v);
        if (vertxList.ContainsKey(v.index))
        {
            vertxList[v.index].adj.Add(e);
            vertxList[w.index].adj.Add(e);
        }
        edgeCount++;
        CheckCircle();
       
        Debug.Log("添加边:节点" + v.index + "————>节点" + w.index);
        Debug.Log(ToString());
    }

  
    public List<List<Node>> getHalfList()
    {
        List<List<Node>> list = circlelist;
        for (int i = list.Count-1; i >=0 ; i--)
        {
            for (int j = 0; j <list[i].Count-1; j++)
            {
                //Debug.Log(list[i][j].index + "——>" + list[i][j + 1].index+"\n"
                //    + battery.startPoint.index + "——>" + battery.endPoint.index);
                if (battery != null)
                {
                    Node v = list[i][j];
                    Node w = list[i][j+1];
                   // Debug.Log(v.index+"——>"+w.index);
                    if (list[i][j].index == battery.startPoint.index && list[i][j + 1].index == battery.endPoint.index)
                        list.RemoveAt(i);
                }
            }
        }
        return list;
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
    public Edge GetEdge(int i, int j) {
        return vertxList[i].adj[j];
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
    public override string ToString()
    {
        string s = VertexCount + " 个顶点, " + EdgeCount + " 条边\n";
        for (int i = 0; i < VertexCount; i++)
        {
            s += i + ": ";
            for (int j = 0; j < vertxList[i].adj.Count; j++)
            {
                Edge e = vertxList[i].adj[j];
                s += "节点："+e.other(vertxList[i]).index+" ";
            }
            s += "\n";
        }
        return s;
    }
    public List<List<int>> AdjMatrix
    {
        get
        {
            List<List<int>> matrix1 = new List<List<int>>();
            for (int i = 0; i < VertexCount; i++)
            {
                List<int> matrix2 = new List<int>();
                for (int j = 0; j < VertexCount; j++)
                {
                    if (getVertex(i).hasPathTo(j))
                    {
                        matrix2.Add(1);
                    }
                    else
                    {
                        matrix2.Add(0);
                    }
                }
                matrix1.Add(matrix2);
            }
            return matrix1;
        }
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
    public void setBattery(Element battery) {
        this.battery = battery;
    }
    public void CheckCircle()
    {
        int count = 0;//环的个数  
        int top = -1;
        int[] stack = new int[VertexCount];
        bool[] inStack = new bool[VertexCount];
        bool[] visited = new bool[VertexCount];
        for (int i = 0; i < VertexCount; i++)
        {
            if (!visited[i])
            {
                DFS(i, visited, stack, ref top, inStack, ref count);
            }
        }
        Debug.Log(outCircle());
    }
    public string outCircle() {
        string strResult = "有"+getHalfList().Count+"个环\n";
        for (int i = 0; i < getHalfList().Count; i++) {
            for (int j = 0; j < getHalfList()[i].Count-1; j++)
            {
                strResult += "元器件：" + getHalfList()[i][j].parentName+" 节点"+ getHalfList()[i][j].index+ "——> 元器件"+ getHalfList()[i][j+1].parentName+"节点" + getHalfList()[i][j+1].index;
            }
            strResult += "\n";
        }
        return strResult;
    }
    void DFS(int x, bool[] visited, int[] stack, ref int top, bool[] inStack, ref int count)
    {
        visited[x] = true;
        stack[++top] = x;
        inStack[x] = true;
        circlelist = new List<List<Node>>();
        //string outstr;
        for (int i = 0; i < VertexCount; i++)
        {
            if (AdjMatrix[x][i] != 0)//有边  
            {
                if (!inStack[i])
                {
                    DFS(i, visited, stack, ref top, inStack, ref count);
                }
                else //条件成立，表示下标为x的顶点到 下标为i的顶点有环  
                {
                    count++;
                    // outstr = "第" + count + "环为:";
                    List<Node> circle = new List<Node>();//环
                    List<Edge> circleEdge = new List<Edge>();
                    //从i到x是一个环，top的位置是x，下标为i的顶点在栈中的位置要寻找一下  
                    //寻找起始顶点下标在栈中的位置  
                    int t = 0;
                    for (t = top; stack[t] != i; t--) ;
                    //输出环中顶点  
                    for (int j = t; j <= top; j++)
                    {
                      //  outstr += vertxList[stack[j]].index + "————>";
                        circle.Add(vertxList[stack[j]]);
                        
                    }
                    circle.Add(vertxList[0]);
                   // outstr += "\n";
                    if (circle.Count > 3)
                    {
                        //count++;
                        circlelist.Add(circle);
                        //Debug.Log(outstr);
                    }
                }
            }
        }
        //处理完结点后，退栈  
        top--;
        inStack[x] = false;
    }
}
