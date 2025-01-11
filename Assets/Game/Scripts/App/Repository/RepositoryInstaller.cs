using UnityEngine;
using Zenject;

namespace SampleGame.App.Repository
{
    public class RepositoryInstaller : Installer<RepositoryInstaller>
    {
        private const string Uri = "http://127.0.0.1:8888";
        private const string FileName = "GameState.txt";

        private const string Key = "SomeKey";
        private const string IV = "SomeIV";

        public override void InstallBindings()
        {
            Container.Bind<GameClient>()
                .AsSingle()
                .WithArguments(Uri)
                .NonLazy();

            Container.BindInterfacesTo<GameRepository>()
                .AsSingle()
                .WithArguments($"{Application.streamingAssetsPath}/{FileName}")
                .NonLazy();

            Container.Bind<Encryptor>()
                .AsSingle()
                .WithArguments(Key, IV)
                .NonLazy();
        }
    }
}