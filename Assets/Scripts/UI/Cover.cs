using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public void ClickCover()
    {
        // ScenesManager.GetInstance().LoadScene("Main", ()=>{});
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

}
