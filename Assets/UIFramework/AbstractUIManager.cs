using System.Collections.Generic;
using UnityEngine;

namespace UIFramework
{
    /// <summary>
    /// UI 管理基类
    /// 用于初始化、打开、关闭所管理的UI容器
    /// </summary>
    public abstract class AbstractUIManager : MonoBehaviour
    {
        public AbstractPanel[] Panels;
        [HideInInspector] public AbstractPanel CurrentPanel;

        public AbstractBox[] Boxs;
        [HideInInspector] public List<AbstractBox> CurrentBoxs;

        private void Start()
        {
            CurrentBoxs = new List<AbstractBox>();
            if (Boxs != null && Boxs.Length > 0)
            {
                foreach (var box in Boxs)
                {
                    box.PanelManager = this;
                    box.gameObject.SetActive(false);
                    (box as IUIContainer).Init();
                }
            }
            if (Panels != null && Panels.Length > 0)
            {
                foreach (var panel in Panels)
                {
                    panel.PanelManager = this;
                    (panel as IUIContainer).Init();
                }
                OpenPanel(Panels[0]);
            }
        }

        #region Panel
        private void PanelOpen(AbstractPanel panel)
        {
            panel.PanelBefore = CurrentPanel;
            CurrentPanel = panel;
            CloseAllPanels();
            CloseAllBox();
            (panel as IUIContainer).Open();
        }

        public void OpenPanel(AbstractPanel panel)
        {
            if (panel == null)
            {
                return;
            }
            foreach (var pan in Panels)
            {
                if (pan == panel)
                {
                    PanelOpen(pan);
                    return;
                }
            }
        }

        public void OpenPanel(string panelName)
        {
            if (string.IsNullOrEmpty(panelName))
            {
                return;
            }

            foreach (var panel in Panels)
            {
                if (panel.name == panelName)
                {
                    PanelOpen(panel);
                    return;
                }
            }
        }

        public void OpenPanel<C>() where C : IUIContainer
        {
            foreach (var panel in Panels)
            {
                if (panel.GetType() == typeof(C))
                {
                    PanelOpen(panel);
                    return;
                }
            }
        }

        private void CloseAllPanels()
        {
            if (Panels.Length <= 0)
            {
                return;
            }
            foreach (var panel in Panels)
            {
                if (panel.isActiveAndEnabled)
                {
                    (panel as IUIContainer).Close();
                }
            }
        }

        public P GetPanel<P>() where P : AbstractPanel
        {
            foreach (var panel in Panels)
            {
                if (panel.GetType() == typeof(P))
                {
                    return (P)panel;
                }
            }
            return null;
        }
        #endregion

        #region Box
        public void OpenBox(AbstractBox box)
        {
            if (box == null)
            {
                return;
            }
            if (!CurrentBoxs.Contains(box))
            {
                CurrentBoxs.Add(box);
                box.gameObject.SetActive(true);
                (box as IUIContainer).Open();
            }
        }

        public void OpenBox(string boxName)
        {
            if (string.IsNullOrEmpty(boxName))
            {
                return;
            }
            foreach (var box in Boxs)
            {
                if (box.name == boxName)
                {
                    OpenBox(box);
                    return;
                }
            }
        }

        public void OpenBox<C>()where C : IUIContainer
        {
            foreach (var box in Boxs)
            {
                if (box.GetType() == typeof(C))
                {
                    OpenBox(box);
                    return;
                }
            }
        }

        public void CloseBox(AbstractBox box)
        {
            if (box == null)
            {
                return;
            }
            if (CurrentBoxs.Contains(box))
            {
                CurrentBoxs.Remove(box);
                box.gameObject.SetActive(false);
                (box as IUIContainer).Close();
            }
        }

        public void CloseBox(string boxName)
        {
            if (string.IsNullOrEmpty(boxName))
            {
                return;
            }
            foreach (var box in Boxs)
            {
                if (box.name == boxName)
                {
                    CloseBox(box);
                    return;
                }
            }
        }

        public void CloseBox<C>() where C : IUIContainer
        {
            foreach (var box in Boxs)
            {
                if (box.GetType() == typeof(C))
                {
                    CloseBox(box);
                    return;
                }
            }
        }

        private void CloseAllBox()
        {
            foreach (var box in CurrentBoxs)
            {
                box.gameObject.SetActive(false);
                (box as IUIContainer).Close();
            }
            CurrentBoxs.Clear();
        }

        public B GetBox<B>() where B : AbstractBox
        {
            foreach (var box in Boxs)
            {
                if (box.GetType() == typeof(B))
                {
                    return (B)box;
                }
            }
            return null;
        }
        #endregion
    }
}
