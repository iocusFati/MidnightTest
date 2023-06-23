using CodeBase.Infrastructure.Services.AssetProviderService;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.CameraData;
using CodeBase.Infrastructure.StaticData.UIData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        public CameraStaticData CameraData { get; private set; }
        public PlayerStaticData PlayerData { get; private set; }
        public UIStaticData UIData { get; private set; }

        public void Initialize()
        {
            LoadCameraData();
            LoadPlayerData();
            LoadUIData();
        }

        private void LoadPlayerData() => 
            PlayerData = Resources.Load<PlayerStaticData>(AssetPaths.PlayerData);

        private void LoadCameraData() => 
            CameraData = Resources.Load<CameraStaticData>(AssetPaths.CameraData);
        
        private void LoadUIData() => 
            UIData = Resources.Load<UIStaticData>(AssetPaths.UIData);
    }
}