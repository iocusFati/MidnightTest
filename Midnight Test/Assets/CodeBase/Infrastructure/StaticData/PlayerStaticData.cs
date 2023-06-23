using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/PlayerData")]
    public class PlayerStaticData : ScriptableObject
    {
        [Header("Speed")]
        [SerializeField] public float BaseSpeed;
        [SerializeField] public float AccelerationSpeed;
        
        [Header("Jump")]
        [SerializeField] public float JumpHeight;
        [SerializeField] public float GroundOffset;
        [SerializeField] public float GravityValue;
        [SerializeField] public float Drag;

        [Header("Rotation")]
        [SerializeField] public float RotationPower;
        [SerializeField] public float VerticalRotationPower;
        [SerializeField] public float VerticalLowLimitRotation;
        [SerializeField] public float VerticalHighLimitRotation;

        [Header("Animation")]
        [SerializeField] public float AnimationSmoothTime;
        [SerializeField] public float AimRifleAnimationDuration;
        [SerializeField] public Vector3 RifleAimPosition;
        [SerializeField] public Vector3 RifleAimRotation;
        [SerializeField] public Vector3 RifleIdlePosition;
        [SerializeField] public Vector3 RifleIdleRotation;
    }
}