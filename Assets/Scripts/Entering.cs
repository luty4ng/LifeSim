using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entering : MonoBehaviour
{
    public void StartGame()
    {
        ScenesManager.GetInstance().LoadScene("Main", ()=>{});
    }
}
