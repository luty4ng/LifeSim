using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Michsky.UI.ModernUIPack;
public class ConditionPanel : MonoBehaviour
{
    [SerializeField] private Protagonist _protagonist;
    private Dictionary<string, ProgressBar> _progressBar;
    private float[] module;
    void Start()
    {
        
        _protagonist = GameManager.instance.playerAgent.GetComponent<Protagonist>();
        _progressBar = new Dictionary<string, ProgressBar>();

        ProgressBar[] viewers = transform.Find("Content").GetComponentsInChildren<ProgressBar>();
        foreach (var item in viewers)
        {
            item.speed = 0;
            _progressBar.Add(item.name, item);
        }
        
        module = _protagonist.GetModule();
        _progressBar["PB - Health"].currentPercent = _protagonist.GetHealth();
        _progressBar["PB - Sans"].currentPercent = _protagonist.GetSans();
        _progressBar["PB - Motor"].currentPercent = module[0];
        _progressBar["PB - Nerve"].currentPercent = module[1];
        _progressBar["PB - Endoc"].currentPercent = module[2];
        _progressBar["PB - Circul"].currentPercent = module[3];
        _progressBar["PB - Breath"].currentPercent = module[4];
        _progressBar["PB - Digest"].currentPercent = module[5];
        _progressBar["PB - Urinary"].currentPercent = module[6];
        _progressBar["PB - Reprod"].currentPercent = module[7];

        _progressBar["PB - Health"].maxValue = _protagonist.maxHealth;
        _progressBar["PB - Sans"].maxValue = _protagonist.maxSans;
        _progressBar["PB - Motor"].maxValue = _protagonist.maxMoudle;
        _progressBar["PB - Nerve"].maxValue = _protagonist.maxMoudle;
        _progressBar["PB - Endoc"].maxValue = _protagonist.maxMoudle;
        _progressBar["PB - Circul"].maxValue = _protagonist.maxMoudle;
        _progressBar["PB - Breath"].maxValue = _protagonist.maxMoudle;
        _progressBar["PB - Digest"].maxValue = _protagonist.maxMoudle;
        _progressBar["PB - Urinary"].maxValue = _protagonist.maxMoudle;
        _progressBar["PB - Reprod"].maxValue = _protagonist.maxMoudle;
    }

    void UpdatePanel()
    {

    }

}
