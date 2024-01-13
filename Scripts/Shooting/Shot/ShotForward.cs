using UnityEngine;

namespace ScrollShooter
{
    [CreateAssetMenu(fileName = "newShotType", menuName = "Configs/ShotConfig/ShotForward")]
    public class ShotForward : ShotConfig
    {
        public override ShotType Type() => ShotType.Forward;
        [Range(0.3f, 10f)] public float BulletsDistance; // MIN = RADIUS BULLET
        
        public override void UpShot()
        {
            Bullets += 1;
        }
        
        public override void Copy(ShotConfig config)
        {
            Prefab = config.Prefab;
            Bullets = config.Bullets;
            BulletsDistance = ((ShotForward)config).BulletsDistance;
        }
        
        public override void ResetModification()
        {
            Bullets = DeffaultBullets;
            BulletsDistance = 0.3f;
        }
    }
}