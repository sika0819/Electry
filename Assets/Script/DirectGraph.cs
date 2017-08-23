using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectGraph {//电流图
    private ElecEdge battery;
    private Dictionary<int,Vertex> vertexArray;//顶点表
    private int vertexCount {
        get {
            return vertexArray.Count;
        }
    }
    private int edgeCount; // 边的数目  

    public List<List<Vertex>> circlelist;
    static int w = 0;
    public DirectGraph()
    {
        edgeCount = 0;
        vertexArray = new Dictionary<int, Vertex>();
        circlelist = new List<List<Vertex>>();
    }
    public DirectGraph(ElecEdge battery)
    {
        edgeCount = 0;
        vertexArray = new Dictionary<int, Vertex>();
        this.battery = battery;
        addVertex(battery.getFrom());
        addVertex(battery.getTo());
        addEdge(battery);
    }
    public void SetBattery(ElecEdge battery) {
        this.battery = battery;
    }
    public void addVertex(Vertex vertex) {
        if (!vertexArray.ContainsKey(vertex.index))
        {
            vertexArray.Add(vertex.index, vertex);
            Debug.Log("添加节点:"+vertex.index);
        }
        else {
            Debug.LogError("字典重复"+vertex.index);
        }
    }
    public void addEdge(ElecEdge e)
    {
        if (vertexArray.ContainsKey(e.getFrom().index))
        {
            vertexArray[e.getFrom().index].adj.Add(e);
        }
        else
        {
            addVertex(e.getFrom());
            if (!vertexArray.ContainsKey(e.getTo().index))
                addVertex(e.getTo());
            vertexArray[e.getFrom().index].adj.Add(e);
        }
        Debug.Log("添加边:节点"+e.getFrom().index+"————>节点"+e.getTo().index);
        CheckCircle();
        edgeCount++;
       
    }
    
    public void addEdge(int from, int to) {
        ElecEdge e = new ElecEdge(vertexArray[from], vertexArray[to]);
        addEdge(e);
    }
    public int getVertexCount()
    {
        return vertexCount;
    }

    public int getEdgeCount()
    {
        return edgeCount;
    }
    public Vertex getVertex(int index) {
        if (index >= 0 && index < vertexArray.Count&&vertexArray!=null)
            return vertexArray[index];
        else
            throw new KeyNotFoundException("vertex " + index + " is not between 0 and " + (vertexCount - 1));
    }
    public Vertex getVertex(string name)
    {
        for (int i = 0; i < vertexArray.Count;i++) {
            if (vertexArray[i].name == name)
                return vertexArray[i];
        }
        return null;
    }
    public List<ElecEdge> getAdj(int v)
    {
        return vertexArray[v].adj;
    }
    public int outdegree(int v)
    {
        getVertex(v);
        return getAdj(v).Count;
    }
    public List<List<ElecEdge>> Edges
    {
        get
        {
            List<List<ElecEdge>> edges = new List<List<ElecEdge>>();
            for (int i = 0; i < vertexCount; i++)
            {
                edges.Add(getAdj(i));
            }
            return edges;
        }
    }
    public List<List<int>> AdjMatrix {
        get {
            List<List<int>> matrix1 = new List<List<int>>();
            for (int i = 0; i < vertexCount; i++) {
                List<int> matrix2 = new List<int>();
                for (int j = 0; j < vertexCount; j++) {
                    if (getVertex(i).hasPathTo(j))
                    {
                        matrix2.Add(1);
                    }
                    else {
                        matrix2.Add(0);
                    }
                }
                matrix1.Add(matrix2);
            }
            return matrix1;
        }
    }
    public void CheckCircle()
    {
        int count = 0;//环的个数  
        int top = -1;
        int[] stack=new int[vertexCount];
        bool[] inStack = new bool[vertexCount];
        bool[] visited = new bool[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            if (!visited[i])
            {
                DFS(i, visited, stack,ref top, inStack,ref count);
            }
        }
    }
    void DFS(int x, bool[] visited, int[] stack, ref int top, bool[] inStack, ref int count)
    {
        visited[x] = true;
        stack[++top] = x;
        inStack[x] = true;
        string outstr;
        for (int i = 0; i < vertexCount; i++)
        {
            if (AdjMatrix[x][i] != 0)//有边  
            {
                if (!inStack[i])
                {
                    DFS(i, visited, stack,ref top, inStack,ref count);
                }
                else //条件成立，表示下标为x的顶点到 下标为i的顶点有环  
                {
                    count++;
                    outstr = "第" + count + "环为:";
                    List<Vertex> circle=new List<Vertex>();//环
                    //从i到x是一个环，top的位置是x，下标为i的顶点在栈中的位置要寻找一下  
                    //寻找起始顶点下标在栈中的位置  
                    int t = 0;
                    for (t = top; stack[t] != i; t--) ;
                    //输出环中顶点  
                    for (int j = t; j <= top; j++)
                    {
                        outstr+= vertexArray[stack[j]].index;
                        circle.Add(vertexArray[stack[j]]);
                    }
                    outstr+="\n";
                    circlelist.Add(circle);
                    Debug.Log(outstr);
                }
            }
        }
        //处理完结点后，退栈  
        top--;
        inStack[x] = false;
    }
    public string toString()
    {
        string s = vertexCount + " 个顶点, " + edgeCount + " 条边\n";
        for (int i = 0; i < vertexCount; i++)
        {
            s += i + ": ";
            for (int j = 0; j < vertexArray[i].adj.Count; j++)
            {
                ElecEdge e = vertexArray[i].adj[j];
                s += "电子元器件："+e.name+" 电压["+e.Voltage+"] 电流["+e.Electry+"] 电阻:["+e.Resistance+"]";
            }
            s += "\n";
        }
        return s;
    }
    public DirectGraph reverse()
    {
        DirectGraph R = new DirectGraph();
        for (int v = 0; v < vertexCount; v++)
        {
            for (int w=0;w< getAdj(v).Count;v++)
            {
                R.addEdge(w, v);
            }
        }
        return R;
    }
    public void OnDestory() {
        vertexArray.Clear();
        circlelist.Clear();
    }
    public bool isSeries() {
        bool isseries = false;//是串联
        if (circlelist.Count == 1) {
            isseries = isLinkToBattery(circlelist[0]);
        }
        return isseries;
    }
    public bool isLinkToBattery(List<Vertex> circle) {
        for (int i = 0; i < circle.Count - 1; i++) {
            if (circle[i].index == battery.getFrom().index && circle[i + 1].index == battery.getTo().index)
            {
                return true;
            }
        }
        Debug.LogError("该线路没有与电池连接！！");
        return false;
    }
    public bool HasPath(Vertex v1, Vertex v2)
    {
        
        return false;
    }
}
