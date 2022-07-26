using UnityEngine;
using UnityEngine.Events;

namespace UIFramework
{
    public abstract class UIBoxBase<T> : MonoBehaviour, IUIContainer where T : UIPanelManagerBase<T>
    {   
        [HideInInspector] public T PanelManager { get; set; }

        /// <summary>
        /// 打开时调用
        /// </summary>
        protected virtual void OnOpen() { }

        /// <summary>
        /// 关闭时调用
        /// </summary>
        protected virtual void OnClose() { }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init() { }

        void IUIContainer.Open()
        {
            OnOpen();
        }

        void IUIContainer.Close()
        {
            OnClose();
        }

        void IUIContainer.Init()
        {
            Init();
        }
    }
}
