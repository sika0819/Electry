using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenrateIndex {
    public GenrateIndex() {
        index = 0;
    }
    public static GenrateIndex Instance {
        get{
            if (_instance == null)
                _instance = new GenrateIndex();
            return _instance;
        }
    }
    private static GenrateIndex _instance;
    private static int index=0;
    public  int Index {
        get
        {
            Debug.Log(index);
            return index++;
        }
    }
    public void OnDestory() {
        index = 0;
    }
}
