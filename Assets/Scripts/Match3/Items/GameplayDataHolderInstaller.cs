using MInject.Runtime.Installer;
using UnityEngine;

namespace Match3.Items
{
    
    [CreateAssetMenu(menuName = "Scriptable Objects/Installer/" + nameof(GameplayDataHolderInstaller), fileName = "GameplayDataHolderInstaller")]
    public class GameplayDataHolderInstaller : ScriptableInstallerBase
    {
        [SerializeField] private GameplayDataHolder GameplayDataHolder;
        public override void InstallBindings()
        {
            ContextBase.RegisterService(GameplayDataHolder);
        }
    }
}