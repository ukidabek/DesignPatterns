using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.MVC.General
{
    public class GeneralTextView : GeneralPropertyView
    {
        [SerializeField] private Text m_text = null;

        public override void OnObjectChanged()
        {
            object value = GetProperty().GetValue(Object);
            m_text.text = value.ToString();
        }
    }
}