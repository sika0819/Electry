using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour {
    public static GameManager Instance {
        get {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GameManager>();
            if (_instance == null)
            {
                GameObject temp = new GameObject();
                temp.AddComponent<GameManager>();
                _instance = temp.GetComponent<GameManager>();
            }
            return _instance;
        }
    }
    private static GameManager _instance;
    private bool isCreateRopeStart=false;
    GameObject cursorObj;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 创建物体
    /// </summary>
    /// <param name="GameObjName"></param>
    public void CreateGameObj(GameObject GameObjName)
    {
        
    }

    /// <summary>
    /// 销毁物体
    /// </summary>
    /// <param name="createObjName"></param>
    public void DestoryGameObj(string createObjName)
    {
        Element createdElement = CreateElement.Instance.GetElement(createObjName);
        if (createdElement != null)
        {
            ResourceTool.DestoryGameObject(createdElement.EleObj);
            CreateElement.Instance.RemoveElement(createdElement);
            createdElement.Destory();
            createdElement = null;
        }
    }

    /// <summary>
    /// 退出
    /// </summary>
    void OnApplicationQuit()
    {
        CreateElement.Instance.OnDestory();
        GenrateIndex.Instance.OnDestory();
    }
}
