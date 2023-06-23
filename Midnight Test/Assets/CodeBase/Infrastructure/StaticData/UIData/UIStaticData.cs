using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.UIData
{
    [CreateAssetMenu(fileName = "UIData", menuName = "StaticData/UIData")]
    public class UIStaticData : ScriptableObject
    {
        [Header("LoseWindow")] 
        public Color ReplayLabelColor1;
        public Color ReplayLabelColor2;
        public float ReplayBlinkDuration;
    }
}