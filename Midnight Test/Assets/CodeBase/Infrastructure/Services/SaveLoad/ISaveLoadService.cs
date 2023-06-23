using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        PlayerProgress LoadProgress();
        void SaveProgress();
        void Register(ISavedProgressReader reader);
        void InformReaders();
    }
}