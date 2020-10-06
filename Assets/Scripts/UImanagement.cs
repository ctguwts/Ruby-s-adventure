using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanagement : MonoBehaviour
{
    //单例模式
    public Image healthBar;

    public static UImanagement instance{get;private set;}
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(int curAmount,int maxAmount){
        healthBar.fillAmount = (float)curAmount/(float)maxAmount;
    }
}
