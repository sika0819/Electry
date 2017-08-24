using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePoint : MonoBehaviour {
    bool isMove=false;
    UltimateRope ropeController;
    Camera expCamera;
    Node linkedPoint;
    Rope rope;
    public GameObject linkObj {
        get {
            return _linkObj;
        }
        set {
            if (_linkObj != value) {
                _linkObj = value;
            }
        }
    }
    [SerializeField]
    private GameObject _linkObj;
    bool isLink=false;
    // Use this for initialization
    void Start () {
        ropeController = transform.parent.GetComponentInChildren<UltimateRope>();
        expCamera = GameObject.FindGameObjectWithTag(ResourceTool.EXPCAMERA).GetComponent<Camera>();
        rope = CreateElement.Instance.GetRope(transform.parent.name);
    }
	
	// Update is called once per frame
	void Update () {
        if (isLink) {
            if(linkObj!=null)
            transform.position = linkObj.transform.position;
        }
        if (isMove)
        {
            Ray ray = expCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                ropeController.Regenerate(false);
                if (rope.IsLinked) {
                    
                    Debug.Log("bling~连接");
                }
                isLink = false;
            }

        }
        if (Input.GetMouseButtonUp(0)) {
            isMove = false;
        }
	}
    void OnMouseDown() {
        isMove = true;
    }
    void OnTriggerEnter(Collider coli)
    {
        
        if (coli.tag == "Point" && coli.gameObject.transform.parent != transform.parent)
        {
            if (!isLink)
            {
                linkObj = coli.gameObject;
                Node linkPoint= CreateElement.Instance.GetPoint(linkObj.name);
                isMove = false;
                transform.position = coli.gameObject.transform.position;
                //  Debug.Log("bling~连接");
                isLink = true;
                linkedPoint = linkPoint;
                rope.linkNode = linkedPoint;
            }
        }
    }
}
