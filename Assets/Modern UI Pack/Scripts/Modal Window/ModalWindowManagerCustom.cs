using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

namespace Michsky.UI.ModernUIPack
{
    public class ModalWindowManagerCustom : MonoBehaviour
    {
        // Resources
        public Button confirmButton;
        public Animator mwAnimator;

        // Content
        public Sprite icon;
        public string titleText = "Title";
        [TextArea] public string descriptionText = "Description here";

        // Events
        public UnityEvent onConfirm;
        // Settings
        public bool sharpAnimations = false;
        public bool useCustomValues = false;

        public bool isOn = false;

        void Start()
        {
            if (mwAnimator == null)
                mwAnimator = gameObject.GetComponent<Animator>();

            if (confirmButton != null)
                confirmButton.onClick.AddListener(onConfirm.Invoke);
    
        }

        public void UpdateUI()
        {

        }

        public void OpenWindow()
        {
            if (isOn == false)
            {
                if (sharpAnimations == false)
                    mwAnimator.CrossFade("Fade-in", 0.1f);
                else
                    mwAnimator.Play("Fade-in");

                isOn = true;
            }
        }

        public void CloseWindow()
        {
            if (isOn == true)
            {
                if (sharpAnimations == false)
                    mwAnimator.CrossFade("Fade-out", 0.1f);
                else
                    mwAnimator.Play("Fade-out");

                isOn = false;
            }
        }

        public void AnimateWindow()
        {
            if (isOn == false)
            {
                if (sharpAnimations == false)
                    mwAnimator.CrossFade("Fade-in", 0.1f);
                else
                    mwAnimator.Play("Fade-in");

                isOn = true;
            }

            else
            {
                if (sharpAnimations == false)
                    mwAnimator.CrossFade("Fade-out", 0.1f);
                else
                    mwAnimator.Play("Fade-out");

                isOn = false;
            }
        }
    }
}