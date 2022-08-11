using UnityEngine;

namespace UIFramework
{
    /// <summary>
    /// Box UI容器
    /// 用于小窗口，可以随时打开和关闭，当打开新Panel时关闭所有Box
    /// </summary>
    public abstract class AbstractBox : MonoBehaviour, IUIContainer
    {   
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
