using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Factor
{
    public float _motor, _nerve, _endoc, _circul, _breath, _digest, _urinary, _reprod;
}


[CreateAssetMenu(fileName = "New Habbit", menuName = "Habbit")] 
public class HabbitScriptable : ScriptableObject
{
    
    public List<Factor> _eat;
    public List<float> _sleep;
    public List<float> _sport;
    public List<float> _work;

}
