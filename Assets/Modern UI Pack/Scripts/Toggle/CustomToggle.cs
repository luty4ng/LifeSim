using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Michsky.UI.ModernUIPack
{
    [RequireComponent(typeof(Toggle))]
    [RequireComponent(typeof(Animator))]
    public class CustomToggle : MonoBehaviour
    {
        public Toggle toggleObject;
        public Animator toggleAnimator;
        public TextMeshProUGUI toggleSelection;
        public Image behavIcon;

        void Awake()
        {
            if (toggleObject == null)
                toggleObject = gameObject.GetComponent<Toggle>();
           
            if (toggleAnimator == null)
                toggleAnimator = toggleObject.GetComponent<Animator>();
            
            if(toggleSelection == null)
                toggleSelection = this.GetComponentInChildren<TextMeshProUGUI>();

            toggleObject.onValueChanged.AddListener(UpdateStateDynamic);
            UpdateState();
        }

        void OnEnable()
        {
            if (toggleObject == null)
                return;

            UpdateState();
        }

        public void UpdateState()
        {
            if (toggleObject.isOn)
                toggleAnimator.Play("Toggle On");
            else
                toggleAnimator.Play("Toggle Off");
        }

        void UpdateStateDynamic(bool value)
        {
            if (toggleObject.isOn)
                toggleAnimator.Play("Toggle On");
            else
                toggleAnimator.Play("Toggle Off");
        }
    }
}