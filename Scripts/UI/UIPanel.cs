using UnityEngine;

namespace ScrollShooter
{
    public abstract class UIPanel : MonoBehaviour
    {
        public abstract void Activate();
        public abstract void UpdatePanel();
    }
}