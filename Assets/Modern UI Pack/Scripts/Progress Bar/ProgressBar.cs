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
        public float targetValue = 100;


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
                if (currentPercent < targetValue && targetValue - currentPercent >= 0.01f)
                {
                    currentPercent += speed * Time.deltaTime;
                }
                else if (currentPercent > targetValue && targetValue - currentPercent <= -0.01f)
                {
                    currentPercent -= speed * Time.deltaTime;
                }
                else
                {
                    currentPercent = Mathf.Round(currentPercent);
                }
                    

                if (currentPercent >= maxValue && speed != 0 && restart == true)
                    currentPercent = maxValue;
                else if (currentPercent <= 0 && speed != 0 && restart == true)
                    currentPercent = 0;

                loadingBar.fillAmount = currentPercent / maxValue;
                textPercent.text = ((int)currentPercent).ToString("F0");
            }
        }

        public void UpdateUI()
        {
            loadingBar.fillAmount = currentPercent / maxValue;
            textPercent.text = ((int)currentPercent).ToString("F0");
        }
    }
}