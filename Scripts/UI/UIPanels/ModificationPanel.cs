using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ScrollShooter
{
    public class ModificationPanel : UIPanel
    {
        [SerializeField] private Sprite[] _choice = new Sprite[5];
        [SerializeField] private Button[] _buttons = new Button[]{};
        private List<UnityAction> _modifications = new List<UnityAction>();

        private GameManager _gameManager;

        private void OnDisable()
        {
            foreach (Button button in _buttons)
                button.onClick.RemoveAllListeners();
        }

        public override void Activate()
        {
            _gameManager = GameManager.Instance;
            FillModificationsMethods();

            if (_buttons.Length <= 0)
                _buttons = GetComponentsInChildren<Button>();
            
            gameObject.SetActive(true);
            
            int random_Modification = Random.Range(0, 2);
            InitButton(0, random_Modification);
            
            random_Modification = Random.Range(2, 4);
            InitButton(1, random_Modification);

            random_Modification= Random.Range(4, 5);
            InitButton(2, random_Modification);
        }

        public override void UpdatePanel()
        {
        }

        private void InitButton(int index, int modification)
        {
            _buttons[index].onClick.AddListener(_modifications[modification]);
            _buttons[index].GetComponent<Image>().sprite = _choice[modification];
        }

        private void FillModificationsMethods()
        {
            _modifications.Clear();
            _modifications.Add(Modification_1);
            _modifications.Add(Modification_2);
            _modifications.Add(Modification_3);
            _modifications.Add(Modification_4);
            _modifications.Add(Modification_5);
        }

        private void Modificated()
        {
            _gameManager.OnModificated();
            gameObject.SetActive(false);
        }

        private void Modification_1()
        {
            _gameManager.Player.UpBulletSpeed();
            Modificated();
        }
        
        private void Modification_2()
        {
            _gameManager.Player.UpModification(ShotType.Forward);
            Modificated();
        }
        
        private void Modification_3()
        {
            _gameManager.Player.UpModification(ShotType.Angle);
            Modificated();
        }
        
        private void Modification_4()
        {
            _gameManager.Player.AddShield();
            Modificated();
        }
        
        private void Modification_5()
        {
            _gameManager.Player.UpMiniShooters();
            Modificated();
        }
    }
}