using UnityEngine;

namespace UIFramework
{
    public abstract class UIPanelBase<T> : MonoBehaviour, IUIContainer where T : UIPanelManagerBase<T>
    {
        [HideInInspector] public UIPanelBase<T> PanelBefore;

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
            gameObject.SetActive(true);
            OnOpen();
        }

        void IUIContainer.Close()
        {
            gameObject.SetActive(false);
            OnClose();
        }

        void IUIContainer.Init()
        {
            Init();
        }
    }
}
