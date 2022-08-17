using UnityEngine;

namespace UIFramework
{
    public interface IUIContainer
    {
        public AbstractUIManager PanelManager { get; }

        void Init(AbstractUIManager manager);

        void Open(bool immed = false);

        void Close(bool immed = false);
    }

    public abstract class AbstractUIContainer : MonoBehaviour, IUIContainer
    {
        [HideInInspector] public AbstractUIManager PanelManager { get; private set; }

        protected Animator _anim;

        private bool _initialized = false;

        void IUIContainer.Init(AbstractUIManager manager)
        {
            PanelManager = manager;
        }

        void IUIContainer.Open(bool immed)
        {
            InitProperty();

            if (!immed)
            {
                if (_anim && _anim.isActiveAndEnabled)
                {
                    _anim.SetBool("Open", true);
                }
            }
            gameObject.SetActive(true);
            OnOpen();
        }

        void IUIContainer.Close(bool immed)
        {
            if (immed)
            {
                gameObject.SetActive(false);
                OnClose();
            }
            else
            {
                StartCoroutine(CloseDelayed());
            }

            System.Collections.IEnumerator CloseDelayed()
            {
                if (_anim && _anim.isActiveAndEnabled)
                {
                    bool closedStateReached = false;
                    _anim.SetBool("Open", false);
                    while (!closedStateReached)
                    {
                        if (_anim.isActiveAndEnabled && !_anim.IsInTransition(0))
                        {
                            closedStateReached = _anim.GetCurrentAnimatorStateInfo(0).IsName("Closed");
                        }
                        yield return new WaitForEndOfFrame();
                    }
                }
                gameObject.SetActive(false);
                OnClose();
            }
        }

        private void InitProperty()
        {
            if (!_initialized)
            {
                _anim = GetComponent<Animator>();
                OnInit();
                _initialized = true;
            }
        }

        protected abstract void OnInit();

        protected abstract void OnOpen();

        protected abstract void OnClose();
    }
}
