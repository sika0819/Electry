using EasyAR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyTargetBehavior : ImageTargetBehaviour
{
    private GameObject ExperimentArea;
    private string targetName="";
    private GameObject createObj;
    protected override void Awake()
    {
        base.Awake();
        TargetFound += OnTargetFound;
        TargetLost += OnTargetLost;
        TargetLoad += OnTargetLoad;
        TargetUnload += OnTargetUnload;
        ExperimentArea = GameObject.Find(ResourceTool.EXPERIMENT + ResourceTool.EXPAREA);
        ResourceTool.InitResources();
    }
    protected override void Update()
    {
        if (createObj != null) {
            createObj.transform.position = transform.position;
            if (!createObj.name.Contains(ResourceTool.WANYONGBIAO))
            {
                createObj.transform.rotation = transform.rotation;
            }
            else {
                createObj.transform.rotation = Quaternion.Euler(90+transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
        }
    }
    void OnTargetFound(TargetAbstractBehaviour behaviour)
    {
        targetName= Target.Name.Replace(ResourceTool.IMAGE_TARGET, "");
        if (createObj == null)
        {
            switch (targetName)
            {
                case ResourceTool.ENERGYBTN:
                    createObj = ResourceTool.InstitateGameObject(ResourceTool.EnergyPreb);
                    break;
                case ResourceTool.LINEBTN:
                    createObj = ResourceTool.InstitateGameObject(ResourceTool.LinePreb);
                    break;
                case ResourceTool.SWICTHBTN:
                    createObj = ResourceTool.InstitateGameObject(ResourceTool.SwitchPreb);
                    break;
                case ResourceTool.RESISTANCEBTN:
                    createObj = ResourceTool.InstitateGameObject(ResourceTool.ResistancePreb);
                    break;
                case ResourceTool.LIGHTBTN:
                    createObj = ResourceTool.InstitateGameObject(ResourceTool.LightPreb);
                    break;
                case ResourceTool.VOLTMETERBTN:
                    createObj = ResourceTool.InstitateGameObject(ResourceTool.WanYongBiaoPreb);
                    break;
            }
        }
        createObj.transform.localScale = transform.localScale;
    }

    void OnTargetLost(TargetAbstractBehaviour behaviour)
    {
        if (createObj != null) {
            ResourceTool.DestoryGameObject(createObj);
        }
    }

    void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {

    }

    void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {
    }
}
