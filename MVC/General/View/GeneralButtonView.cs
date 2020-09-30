using System;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.MVC.General
{
    public class GeneralButtonView : View<ButtonViewModel>
    {
        [SerializeField] private Button m_button = null;
        [SerializeField] private SignalType m_signalType = null;

        private void OnEnable()
        {
            m_button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            m_button.onClick.RemoveListener(OnClick);
        }

        public override void OnObjectChanged()
        {
            switch (Object.Status)
            {
                case ButtonViewModel.ButtonViewStatus.Available:
                    m_button.interactable = true;
                    gameObject.SetActive(true);
                    break;
                case ButtonViewModel.ButtonViewStatus.Unavailable:
                    m_button.interactable = false;
                    break;
                case ButtonViewModel.ButtonViewStatus.Hidden:
                    gameObject.SetActive(false);
                    m_button.interactable = false;
                    break;
            }
        }

        private void OnClick()
        {
            PropagateSignal(transform.parent);
        }

        private void PropagateSignal(Transform transform)
        {
            Controller controller = transform.gameObject.GetComponent<Controller>();
            if (controller == null)
                PropagateSignal(transform.parent);
            else
                controller.HandleSignal(new Signal(Object?.Context, m_signalType));

        }
    }
}