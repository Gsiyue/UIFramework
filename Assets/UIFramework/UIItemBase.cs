using UnityEngine;

namespace UIFramework
{
    public abstract class UIItemBase<T> : MonoBehaviour where T : IUIContainer
    {
        [HideInInspector] public T Parent;

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            Parent = transform.GetComponentInParent<T>();
            if (Parent == null)
            {
                Debug.LogError(GetType() + " 非 "+ typeof(T) + "  的子物体");
            }
        }
    }
}
