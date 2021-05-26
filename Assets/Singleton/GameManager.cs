using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Protagonist playerAgent;
    public GameObject cover;
    public static GameManager instance {get; private set;}
    private void Awake()
    {
        playerAgent = GameObject.Find("Protagonist").GetComponent<Protagonist>();
        cover = GameObject.Find("Cover");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        InputManager.GetInstance().SetActive(true);
    }
}
