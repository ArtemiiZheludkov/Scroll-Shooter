using UnityEngine;

namespace ScrollShooter
{
    [CreateAssetMenu(fileName = "newCube", menuName = "Configs/CubeConfig")]
    public class CubeConfig : ScriptableObject
    {
        [field: Header("ANIMATION SETTINGS")]
        [field: Range(0f, 10f)]
        [field: SerializeField] public float AnimationTime { get; private set; }
        
        [field: Range(0f, 10f)]
        [field: SerializeField] public float ScaleFactorOnHit { get; private set; }
        
        [field: SerializeField] public float RotateYFactorOnHit { get; private set; }
    }
}