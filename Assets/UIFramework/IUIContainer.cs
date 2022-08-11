using UnityEngine;

namespace UIFramework
{
    public interface IUIContainer
    {
        [HideInInspector] public AbstractUIManager PanelManager { get; set; }

        void Open();

        void Close();

        void Init();
    }
}
