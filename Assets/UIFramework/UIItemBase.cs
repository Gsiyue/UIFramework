using UnityEngine;

namespace UIFramework
{
    /// <summary>
    /// Item 一般用于列表中
    /// </summary>
    public abstract class AbstractItem : MonoBehaviour
    {
        [HideInInspector] public IUIContainer Parent { get; private set; }

        public void Init(IUIContainer container)
        {
            Parent = container;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void OnInit() { }
    }
}
