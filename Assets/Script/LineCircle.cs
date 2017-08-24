using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LineCircle {
    int[] visited;//通过visited数组来标记这个顶点是否被访问过，0表示未被访问，1表示被访问  
    int DFS_Count;//连通部件个数，用于测试无向图是否连通，DFS_Count=1表示只有一个连通部件，所以整个无向图是连通的  
    int[] pre;
    int[] post;
    int point;//pre和post的值
    public LineCircle(LineGraph g)
    {
        int i;
        visited = new int[g.VertexCount];
        pre = new int[g.VertexCount];
        post = new int[g.VertexCount];
        //初始化visited数组，表示一开始所有顶点都未被访问过  
        for (i = 0; i < g.VertexCount; i++)
        {
            visited[i] = 0;
            pre[i] = 0;
            post[i] = 0;
        }
        //初始化pre和post  
        point = 0;
        //初始化连通部件数为0  
        DFS_Count = 0;
        //深度优先搜索  
        for (i = 0; i < g.VertexCount; i++)
        {
            if (visited[i] == 0)//如果这个顶点为被访问过，则从i顶点出发进行深度优先遍历  
            {
                DFS_Count++;//统计调用void dfs(graph *g,int i);的次数  
                dfs(g, i);
            }
        }
    }
    void dfs(LineGraph g, int i)
    {
        //cout<<"顶点"<<g->v[i]<<"已经被访问"<<endl;  
        Debug.Log("顶点" +i +"已经被访问");
        visited[i] = 1;//标记顶点i被访问  
        pre[i] = ++point;
        for (int j = 0; j < g.VertexCount; j++)
        {
            //if (g.getAdj(j)[i] != 0 && visited[j] == 0)
                dfs(g, j);
        }
        post[i] = ++point;
    }

}
