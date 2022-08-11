using UnityEngine;

namespace UIFramework
{
    /// <summary>
    /// Panel UI容器
    /// 同时只可存在一个Panel，当打开另一个Panel时将关闭当前Panel
    /// </summary>
    public abstract class AbstractPanel : MonoBehaviour, IUIContainer
    {
        [HideInInspector] public AbstractPanel PanelBefore;

        [HideInInspector] public AbstractUIManager PanelManager { get; set; }

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
