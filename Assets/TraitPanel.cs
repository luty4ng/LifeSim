using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitPanel : MonoBehaviour
{
    [SerializeField] private Protagonist _protagonist;
    void Start()
    {
        _protagonist = GameManager.instance.playerAgent.GetComponent<Protagonist>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
