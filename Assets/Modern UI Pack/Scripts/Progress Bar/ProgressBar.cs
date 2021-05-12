using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Michsky.UI.ModernUIPack
{
    public class ProgressBar : MonoBehaviour
    {
        // Content
        public float currentPercent;
        public int speed;
        public float maxValue = 100;
        [SerializeField] private float targetValue;


        // Resources
        public Image loadingBar;
        public TextMeshProUGUI textPercent;

        // Settings
        public bool isOn;
        public bool restart;
        public bool invert;
        public bool isPercent;

        void Start()
        {
            
            if (isOn == false)
            {
                loadingBar.fillAmount = currentPercent / maxValue;
                textPercent.text = ((int)currentPercent).ToString("F0");
                isPercent = false;
            }

        }

        void Update()
        {
            if (isOn == true)
            {
                if (currentPercent <= maxValue && invert == false)
                    currentPercent += speed * Time.deltaTime;
                else if (currentPercent >= 0 && invert == true)
                    currentPercent -= speed * Time.deltaTime;

                if (currentPercent >= maxValue && speed != 0 && restart == true && invert == false)
                    currentPercent = maxValue;
                else if (currentPercent <= 0 && speed != 0 && restart == true && invert == true)
                    currentPercent = 0;

                loadingBar.fillAmount = currentPercent / maxValue;

                if (isPercent == true)
                    textPercent.text = ((int)currentPercent).ToString("F0");
                else
                    textPercent.text = ((int)currentPercent).ToString("F0");
            }
        }

        public void UpdateUI()
        {
            loadingBar.fillAmount = currentPercent / maxValue;
          
            if (isPercent == true)
                textPercent.text = ((int)currentPercent).ToString("F0");
            else
                textPercent.text = ((int)currentPercent).ToString("F0");
        }
    }
}