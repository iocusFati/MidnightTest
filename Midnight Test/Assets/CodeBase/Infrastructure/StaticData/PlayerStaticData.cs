using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/PlayerData")]
    public class PlayerStaticData : ScriptableObject
    {
        [SerializeField] public float BaseSpeed;
        [SerializeField] public float AccelerationSpeed;


        [Header("Jump")]
        [SerializeField] public float JumpHeight;
        [SerializeField] public float GroundOffset;
        [SerializeField] public float GravityValue;
        [SerializeField] public float Drag;
    }
}