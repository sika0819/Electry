using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGraph{//电路图
    private Dictionary<int,Node> vertxList;//邻接表,每个顶点都要有。
    private int edgeCount=0;//边节点数量
    private List<List<Node>> circlelist;
    private List<List<Node>> halfList;
    public Element battery;
    public LineGraph() {
        vertxList = new Dictionary<int, Node>();//储存序号
        circlelist = new List<List<Node>>();
        halfList = new List<List<Node>>();
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
        
        //Debug.Log("添加边:节点" + v.index + "————>节点" + w.index+" 电压"+e.Voltage);
       // Debug.Log(ToString());
    }

    
    public List<List<Node>> getHalfList()
    {
        return halfList;
    }
    public int halfCircleCount {
        get {
            return halfList.Count;
        }
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

    public int CircleCount {
        get {
            return circlelist.Count;
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
        int top = -1;
        int[] stack = new int[VertexCount];
        bool[] inStack = new bool[VertexCount];
        bool[] visited = new bool[VertexCount];
        circlelist = new List<List<Node>>();
        for (int i = 0; i < VertexCount; i++)
        {
            if (!visited[i])
            {
                DFS(i, visited, stack, ref top, inStack);
            }
        }
        halfList = circlelist;
        for (int i = halfList.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < halfList[i].Count - 1; j++)
            {
                if (battery != null)
                {
                    Node v = halfList[i][j];
                    Node w = halfList[i][j + 1];
                    // Debug.Log(v.index+"——>"+w.index);
                    if (halfList[i][j].index == battery.startPoint.index && halfList[i][j + 1].index == battery.endPoint.index)
                    {
                        halfList.RemoveAt(i);
                        break;
                    }
                    else if (!isLinkToBattery(halfList[i]))
                    {
                        halfList.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        //Debug.Log(outCircle());
        Debug.Log(outHalfCircle());
    }
    public string outHalfCircle() {
        string strResult = "有"+halfList.Count+"个环\n";
        for (int i = 0; i < halfList.Count; i++) {
            for (int j = 0; j < halfList[i].Count-1; j++)
            {
                strResult += " 节点" + halfList[i][j].index + "——>" + "节点" + halfList[i][j + 1].index;
            }
            strResult += "\n";
        }
        return strResult;
    }
    public string outCircle()
    {
        string strResult = "有" + circlelist.Count + "个环\n";
        for (int i = 0; i < circlelist.Count; i++)
        {
            for (int j = 0; j < circlelist[i].Count - 1; j++)
            {
                strResult += " 节点" + circlelist[i][j].index + "——>" + "节点" + circlelist[i][j + 1].index;
            }
            strResult += "\n";
        }
        return strResult;
    }
    void DFS(int x, bool[] visited, int[] stack, ref int top, bool[] inStack)
    {
        visited[x] = true;
        stack[++top] = x;
        inStack[x] = true;
        for (int i = 0; i < VertexCount; i++)
        {
            if (AdjMatrix[x][i] != 0)//有边  
            {
                if (!inStack[i])
                {
                    DFS(i, visited, stack, ref top, inStack);
                }
                else //条件成立，表示下标为x的顶点到 下标为i的顶点有环  
                {
                   // outstr = "第" + count + "环为:";
                    List<Node> circle = new List<Node>();//环
                    //从i到x是一个环，top的位置是x，下标为i的顶点在栈中的位置要寻找一下  
                    //寻找起始顶点下标在栈中的位置  
                    int t = 0;
                    for (t = top; stack[t] != i; t--) ;
                    //输出环中顶点  
                    for (int j = t; j <= top; j++)
                    {
                       // outstr += vertxList[stack[j]].index + "————>";
                        circle.Add(vertxList[stack[j]]);
                        
                    }
                    circle.Add(vertxList[stack[t]]);
                    //outstr += stack[t] +"\n";
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
    /// <summary>
    /// 计算串联电路
    /// </summary>
    public void GenerateChuanLian()
    {
        if (isLinkToBattery(circlelist[0]))
        {
            float allResistance = 0;
            for (int i = 0; i < circlelist[0].Count; i++)
            {
                allResistance += circlelist[0][i].Resistance;
            }
            float allElectry = allVoltage / allResistance;
            Debug.Log("电阻：" + allResistance + " 电压:" + allVoltage + " 电流:" + allElectry);
            for (int i = 0; i < circlelist[0].Count; i++)
            {
                for (int j = 0; j < getAdj(circlelist[0][i].index).Count; j++)
                {
                    Edge nowEdge = getAdj(circlelist[0][i].index)[j];
                    nowEdge.Electry = allElectry;
                    // Debug.Log(getAdj(i)[j].name);
                    Element nowElement = CreateElement.Instance.GetElement(nowEdge.name);
                    switch (nowElement.EleType)
                    {
                        case ElementType.Light:
                            ElecLight light = CreateElement.Instance.GetEleLight(nowEdge.name);
                            light.setCurrency(allElectry);
                            light.Electry();
                            break;
                        default:
                            nowElement.Electry();
                            break;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 计算并联，找出环路之间重复的节点，重复的节点电阻沿着环路相加。再将所有环中去掉重复的边，将剩下同环上的相加，不同环按照并联公式计算
    /// </summary>
    public void GenerateBingLian()
    {
        List<List<Edge>> repeatEdge = new List<List<Edge>>();//储存重复节点
        List<List<Node>> nodeList = halfList;
        for (int i = 0; i < halfCircleCount; i++) {
            for (int j = 0; j < nodeList.Count; j++)
            {
                if (i == j)
                    continue;
                for (int halfLoop = 0; halfLoop < halfList[i].Count-1; halfLoop++)
                {
                    List<Edge> onSameCircle = new List<Edge>();
                    for (int copyLoop = 0; copyLoop < nodeList.Count-1; copyLoop++) {
                        if ((halfList[i][halfLoop].index == nodeList[j][copyLoop].index) && (halfList[i][halfLoop + 1].index == nodeList[j][copyLoop + 1].index))
                        {
                            Edge e = new Edge(nodeList[j][copyLoop], nodeList[j][copyLoop+1]);
                            onSameCircle.Add(e);
                        }
                    }
                    repeatEdge.Add(onSameCircle);
                }
            }
        }
    }
    public bool isLinkToBattery(List<Node> circle)
    {
        for (int i = 0; i < circle.Count - 1; i++)
        {
            if (battery != null)
            {
                // Debug.Log("电池："+battery.getFrom().index +"——>"+ battery.getTo().index+"\n"
                //    + circle[i].index + "——>" + circle[i + 1].index);
                if ((circle[i].index == battery.endPoint.index && circle[i + 1].index == battery.startPoint.index))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public float allVoltage
    {
        get
        {
            return battery.Voltage;
        }
    }
}
