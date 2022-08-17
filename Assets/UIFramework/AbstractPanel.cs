using UnityEngine;

namespace UIFramework
{
    /// <summary>
    /// Panel UI容器
    /// 同时只可存在一个Panel，当打开另一个Panel时将关闭当前Panel
    /// </summary>
    public abstract class AbstractPanel : AbstractUIContainer
    {
        [HideInInspector] public AbstractPanel PanelBefore;

        protected override void OnInit() { }

        protected override void OnOpen() { }

        protected override void OnClose() { }
    }
}
