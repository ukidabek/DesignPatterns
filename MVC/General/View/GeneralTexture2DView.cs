using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.MVC.General
{
    public class GeneralTexture2DView : GeneralPropertyView
    {
        [SerializeField] private Image m_image = null; 
        public override bool AcceptObject(object @object) => base.AcceptObject(@object) && m_propertyName.GetType() == typeof(Texture2D);

        public override void OnObjectChanged()
        {
            
            Texture2D texture = GetProperty().GetValue(Object) as Texture2D;
            if (!texture) return;
            Rect rect = new Rect(0f, 0f, texture.width, texture.height);
            m_image.sprite = Sprite.Create(texture, rect, Vector2.one * .5f);
        }
    }
}