using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScrollShooter
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private MenuPanel _menu;
        [SerializeField] private GamePanel _game;
        [SerializeField] private WinPanel _win;
        [SerializeField] private LosePanel _lose;
        [SerializeField] private ModificationPanel _modification;
        
        private Dictionary<Type, UIPanel> _panels;

        public void Init()
        {
            _panels = new Dictionary<Type, UIPanel>();
            _panels.Add(typeof(MenuPanel), _menu);
            _panels.Add(typeof(GamePanel), _game);
            _panels.Add(typeof(WinPanel), _win);
            _panels.Add(typeof(LosePanel), _lose);
            _panels.Add(typeof(ModificationPanel), _modification);
        }

        public void ActivatePanel<T>() where T : UIPanel
        {
            DeactivatePanels();
            _panels[typeof(T)].Activate();
        }

        public void UpdatePanel<T>() where T : UIPanel
        {
            _panels[typeof(T)].UpdatePanel();
        }
        
        public void ActivateModificationPanel()
        {
            _modification.Activate();
        }

        private void DeactivatePanels()
        {
            foreach(UIPanel panel in gameObject.GetComponentsInChildren<UIPanel>()) 
            {
                panel.gameObject.SetActive(false);
            }
        }
    }
}