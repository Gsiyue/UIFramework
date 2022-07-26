using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UIFramework
{
    public abstract class UIPanelManagerBase<T> : MonoBehaviour where T : UIPanelManagerBase<T>
    {
        public UIPanelBase<T>[] Panels;
        [HideInInspector] public UIPanelBase<T> CurrentPanel;

        public UIBoxBase<T>[] Boxs;
        [HideInInspector] public List<UIBoxBase<T>> CurrentBoxs;

        private void Start()
        {
            CurrentBoxs = new List<UIBoxBase<T>>();
            if (Panels != null && Panels.Length > 0)
            {
                foreach (var panel in Panels)
                {
                    panel.PanelManager = (T)this;
                    (panel as IUIContainer).Init();
                }
                OpenPanel(Panels[0]);
            }
            if (Boxs != null && Boxs.Length > 0)
            {
                foreach (var box in Boxs)
                {
                    box.PanelManager = (T)this;
                    box.gameObject.SetActive(false);
                    (box as IUIContainer).Init();
                }
            }
        }

        #region Panel
        public void OpenPanel(UIPanelBase<T> panel)
        {
            if (panel == null)
            {
                return;
            }
            panel.PanelBefore = CurrentPanel;
            CurrentPanel = panel;
            CloseAllPanels();
            CloseAllBox();
            (panel as IUIContainer).Open();
        }

        public void OpenPanel(string panelName)
        {
            if (string.IsNullOrEmpty(panelName))
            {
                return;
            }
            UIPanelBase<T> panel = null;
            for (int i = 0; i < Panels.Length; i++)
            {
                if (Panels[i].name == panelName)
                {
                    panel = Panels[i];
                    break;
                }
            }

            OpenPanel(panel);
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

        public P GetPanel<P>() where P : UIPanelBase<T>
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
        public void OpenBox(UIBoxBase<T> box)
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
                    break;
                }
            }
        }

        public void CloseBox(UIBoxBase<T> box)
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
                    break;
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

        public B GetBox<B>() where B : UIBoxBase<T>
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
