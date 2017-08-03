using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Utils
{
    [RequireComponent(typeof(RectTransform))]
    public class IndentByHeightFitter : UIBehaviour, UnityEngine.UI.ILayoutSelfController
    {
        public enum Edge
        {
            Left,
            Right
        }

        [SerializeField]
        Edge m_Edge = Edge.Left;
        [SerializeField]
        float border;

        public virtual void SetLayoutHorizontal()
        {
            UpdateRect();
        }


        public virtual void SetLayoutVertical() { }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            UpdateRect();
        }
#endif


        protected override void OnRectTransformDimensionsChange()
        {
            UpdateRect();
        }


        Vector2 GetParentSize()
        {
            RectTransform parent = transform.parent as RectTransform;

            return parent == null ? Vector2.zero : parent.rect.size;
        }


        RectTransform.Edge IndentEdgeToRectEdge(Edge edge)
        {
            return edge == Edge.Left ? RectTransform.Edge.Left : RectTransform.Edge.Right;
        }


        void UpdateRect()
        {
            RectTransform rect = (RectTransform)transform;
            Vector2 parentSize = GetParentSize();

            rect.SetInsetAndSizeFromParentEdge(IndentEdgeToRectEdge(m_Edge), parentSize.y + border, parentSize.x - parentSize.y);
        }
    }
}
