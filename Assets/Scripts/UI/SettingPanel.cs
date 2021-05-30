using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            AudioManager.GetInstance().ChangeVolumeByUnit(-0.1f);
        if(Input.GetKeyDown(KeyCode.D))
            AudioManager.GetInstance().ChangeVolumeByUnit(0.1f);
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("关闭游戏");
            Application.Quit();
        }
    }
}
