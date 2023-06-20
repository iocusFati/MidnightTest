using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.StaticData
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
        [SerializeField] public float VerticalLowLimitRotation;
        [SerializeField] public float VerticalHighLimitRotation;
        [SerializeField] public float VerticalRotationPower;
    }
}