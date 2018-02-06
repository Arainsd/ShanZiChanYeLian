using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OrderConfig : MonoBehaviour
{
    public Order order;
    [SerializeField]
    public Star star;
    public int needPartCount = 0;
    //当前订单在栏位中的坐标索引
    public int posIndex = 0;
    //订单完成对应的图片
    public Sprite sprite;
    //单个部件满足数量时对应亮起图片的父级对象
    public Transform rightsTan;
    //将每个订单单独提取出来，每个订单单独和原料中的字典进行对比
    public Dictionary<string, int> orderNumberDict = new Dictionary<string, int>();
    public Dictionary<string, GameObject> rightsDictionary = new Dictionary<string, GameObject>();
    void Awake()
    {
        //点击相应的拼装按钮
        if (this.order.CQ != 0)
        {
            this.orderNumberDict.Add("CQ", this.order.CQ);
            rightsDictionary.Add("CQ", rightsTan.Find("CQ").gameObject);
        }
        if (this.order.DM != 0)
        {
            this.orderNumberDict.Add("DM", this.order.DM);
            rightsDictionary.Add("DM", rightsTan.Find("DM").gameObject);
        }
        if (this.order.JY != 0)
        {
            this.orderNumberDict.Add("JY", this.order.JY);
            rightsDictionary.Add("JY", rightsTan.Find("JY").gameObject);
        }
        if (this.order.SI != 0)
        {
            this.orderNumberDict.Add("SI", this.order.SI);
            rightsDictionary.Add("SI", rightsTan.Find("SI").gameObject);
        }
        if (this.order.SQ != 0)
        {
            this.orderNumberDict.Add("SQ", this.order.SQ);
            rightsDictionary.Add("SQ", rightsTan.Find("SQ").gameObject);
        }
        if (this.order.XY != 0)
        {
            this.orderNumberDict.Add("XY", this.order.XY);
            rightsDictionary.Add("XY", rightsTan.Find("XY").gameObject);
        }
        if (this.order.XYL != 0)
        {
            this.orderNumberDict.Add("XYL", this.order.XYL);
            rightsDictionary.Add("XYL", rightsTan.Find("XYL").gameObject);
        }
        if (this.order.Zhi != 0)
        {
            this.orderNumberDict.Add("Zhi", this.order.Zhi);
            rightsDictionary.Add("Zhi", rightsTan.Find("Zhi").gameObject);
        }
        this.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(
            () =>
                {
                    AssemManager.Instance.CreateAssemSuccessSanzi(this.sprite);
                    TextCountManager.Instance.UpdatePartTextCountAssemLater(this.GetComponent<OrderConfig>());
                    //当每次点击拼装按钮时，重新比较是否满足数量
                    OrderManager.Instance.CompareOrder();
                    //创建新的订单之后再进行原料和部件的对比
                    // this.gameObject.SetActive(false);
                    // 需要把订单移除
                    this.StartAssem();

                    System.GC.Collect();
                });
    }
    void StartAssem()
    {
        //  DictionaryRemove(pKey);
        OrderManager.Instance.currentOrderList.Remove(this.GetComponent<OrderConfig>());
        //创建新的订单
        OrderManager.Instance.CreateNewOrder(this.posIndex, true);
        //如果当前订单完成，则为当前订单增加得分
        ScoreManager.Instance.UpdateScoreText(this.star.GetHashCode());
        //
        OrderManager.Instance.AssemLaterCompare();
        this.gameObject.GetComponent<RectTransform>().DOScale(0, 0.5f);
        Destroy(this.gameObject, 1);
    }
}
