using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0f;
    }
    public void ClickCover()
    {
        // ScenesManager.GetInstance().LoadScene("Main", ()=>{});
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

}
