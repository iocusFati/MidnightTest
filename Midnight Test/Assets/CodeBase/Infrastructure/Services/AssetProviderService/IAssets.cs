using UnityEngine;

namespace CodeBase.Infrastructure.Services.AssetProviderService
{
    public interface IAssets : IService
    {
        TCreatable Instantiate<TCreatable>(string path, Transform parent) where TCreatable : Object;
        TCreatable Instantiate<TCreatable>(string path, Vector3 at) where TCreatable : Object;
        TCreatable Instantiate<TCreatable>(string path) where TCreatable : Object;
    }
}