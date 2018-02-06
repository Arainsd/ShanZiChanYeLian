using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialClickManager : MonoBehaviour
{
    public List<GameObject> halosObj = new List<GameObject>();
    public Dictionary<string, GameObject> haloDictionary = new Dictionary<string, GameObject>();
    public GameObject lastHightLightObj;
    private static MaterialClickManager _instance;
    public static MaterialClickManager Instance
    {
        get { return _instance; }
    }
    public List<DropItem> dropItemList=new List<DropItem>(4);
    public DragItem currentDragItem;
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        for (int i = 0; i < halosObj.Count; i++)
        {
            haloDictionary.Add(halosObj[i].name, halosObj[i]);
        }
    }
    //当按钮被选中时，外边框高亮
    public void HighLightButton(string objName, DragItem dragItem)
    {
        if (lastHightLightObj != null)
        {
            lastHightLightObj.gameObject.SetActive(false);
        }
        GameObject obj = null;
        if (haloDictionary.TryGetValue(objName, out obj))
        {
            obj.gameObject.SetActive(true);
            currentDragItem = dragItem;
            lastHightLightObj = obj;
        }
    }
    //隐藏所有的高亮的按钮
    public void HideHalos()
    {
        for (int i = 0; i < halosObj.Count; i++)
        {
            halosObj[i].SetActive(false);
        }
        if (lastHightLightObj != null)
        {
            lastHightLightObj.SetActive(false);
            lastHightLightObj = null;
        }
        if (currentDragItem != null)
        {
            currentDragItem = null;
        }
    }
    //判断当前是否有按钮处于为被选中的状态
    public bool currentHaveButtonHighLight()
    {
        for (int i = 0; i < halosObj.Count; i++)
        {
            if (halosObj[i].gameObject.activeSelf == true)
            {
                return true;
            }
        }
        return false;
    }
    //检测所有加工栏，没有占用就高亮
    public void CheckDropIfInUse()
    {
        for (int i = 0; i < dropItemList.Count; i++)
        {
            if (dropItemList[i].inUse == false)
            {
                dropItemList[i].highImage.gameObject.SetActive(true);
            }
        }
    }
}
