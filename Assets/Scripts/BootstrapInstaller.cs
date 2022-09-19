using Assets.Scripts.Player;
using Assets.Scripts.Services.PlayerInput;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class BootstrapInstaller : MonoInstaller
    {

        private GameFactory _factory;
        private GameObject _player;

        AssetsProvider _assetsProvider;
        public override void InstallBindings()
        {
            BindInputService();

            InitWorld();
        }

        private async void InitWorld()
        {
            BindAssetProvider();
            BindFactory();
            BindLootData();
            BindPlayerStats();
            await CreateHud();
            await BindPlayer();
            SetCameraFallow();
            CreateEnemySpawner();
              InitializedAssets();
        }

        private void BindAssetProvider()
        {
            Container.Bind<AssetsProvider>()
                         .FromNew()
                         .AsSingle();
        }

        private void InitializedAssets()
        {
              _assetsProvider = Container.Resolve<AssetsProvider>();
              _assetsProvider.Initialized(); 
        }

        private void BindLootData()
        {
              Container.Bind<LootData>()
                          .FromNew()
                          .AsSingle();
        }

        private void SetCameraFallow() =>
            Camera.main.GetComponent<CameraFollow>().SetTarget(_player.transform);

        private async Task CreateHud() => // в фабрику 
            await _factory.CreateHudAsync();

        private void CreateEnemySpawner() =>
            _factory.CreateEnemySpawner();

        private void BindPlayerStats() =>  
           Container.Bind<PlayerStats>().AsSingle();

        private void BindFactory()
        {
            Container.Bind<GameFactory>()
              .FromNew()
              .AsSingle();
            _factory = Container.Resolve<GameFactory>();
        }

        private async Task BindPlayer() // в фабрику
        {
            _player = await _factory.CreatePlayerAsync();
            Container.Bind<PlayerMove>().FromInstance(_player.GetComponent<PlayerMove>());

        }

        private void BindInputService()
        {
            Container
                .Bind<IInputService>()
                 .FromInstance(InputService())
                 .AsSingle();
        }

        private InputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

    }
}
