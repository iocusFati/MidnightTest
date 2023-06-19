using Infrastructure.AssetProviderService;
using Infrastructure.Data;
using Infrastructure.Data.CameraData;
using Infrastructure.Data.UIData;
using Infrastructure.StaticData;
using UnityEngine;

namespace Infrastructure.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        public CameraStaticData CameraData { get; private set; }
        public PlayerStaticData PlayerData { get; set; }

        public void Initialize()
        {
            LoadCameraData();
            LoadPlayerData();
        }

        private void LoadPlayerData() => 
            PlayerData = Resources.Load<PlayerStaticData>(AssetPaths.PlayerData);

        private void LoadCameraData() => 
            CameraData = Resources.Load<CameraStaticData>(AssetPaths.CameraData);
    }
}