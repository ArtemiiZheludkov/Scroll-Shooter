using UnityEngine;

namespace ScrollShooter
{
    public abstract class ShotConfig : ScriptableObject
    {
        public abstract ShotType Type();
        public Bullet Prefab;
        [Range(1, 20)] public int DeffaultBullets;
        public int Bullets { get; protected set; }

        public abstract void UpShot();
        public abstract void Copy(ShotConfig config);
        public abstract void ResetModification();
    }
}