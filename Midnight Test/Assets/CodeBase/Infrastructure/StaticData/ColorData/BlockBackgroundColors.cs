using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using Color = UnityEngine.Color;

namespace CodeBase.Infrastructure.StaticData.ColorData
{
    [Serializable]
    public class BlockBackgroundColors
    {
        public List<Color> BlockPalette;
        [FormerlySerializedAs("BackgroundColors")] [FormerlySerializedAs("TwoColorsPack")] public TwoColorPack BackgroundColor;
    }
}