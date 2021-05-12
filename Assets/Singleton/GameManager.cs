using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject playerAgent;
    public GameObject canvasAgent;
    public static GameManager instance {get; private set;}
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
        }
        
        InputManager.GetInstance().SetActive(true);
    }
}
