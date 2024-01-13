using System.Collections.Generic;
using UnityEngine;

namespace ScrollShooter
{
    [CreateAssetMenu(fileName = "newPlayer", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("MOVING")]
        [Range(0f, 1000f)] public float MoveSpeed;

        [Header("SHOOTING")]
        [Range(1, 100)] public int FireRate;
        [Range(1, 100)] public int BulletDamage;

        [Header("MODIFICATION")]
        public List<ShotConfig> Modification = new List<ShotConfig>();

        [Header("BUFFS")]
        [Range(1f, 100f)] public float DeffaultBulletSpeed;

        [HideInInspector] public float BulletSpeed;
        
        public MiniShooterConfig MiniShootersConfig;
        public bool MiniShootersActivated;
        public bool HasShield;
        public bool HasRespawn;

        [Header("ANIMATION")]
        [Range(0f, 100f)] public float AnimationShootDuration;
        [Range(0, 10)] public int AnimationShootVibrato;
        
        public void RemovedShield() => HasShield = false;

        public void PlayerAddShot(ShotConfig shot)
        {
            bool copyed = false;
            
            foreach (ShotConfig mod in Modification)
            {
                if (mod.Type() == shot.Type())
                {
                    mod.Copy(shot);
                    copyed = true;
                    break;
                }
            }

            if (copyed == false)
                Modification.Add(shot);
        }
    }
}
