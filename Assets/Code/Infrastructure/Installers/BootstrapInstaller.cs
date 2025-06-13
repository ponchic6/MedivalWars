using Code.Gameplay.Input.Services;
using Code.Gameplay.Map.Services;
using Code.Gameplay.Routes.Services;
using Code.Gameplay.Soldiers.Services;
using Code.Gameplay.Towers.Services;
using Code.Infrastructure.Services;
using Code.Infrastructure.StaticData;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;
using Entitas;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private CommonStaticData _commonStaticData;
        
        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;
            Container.Bind<IContext<GameEntity>>().FromInstance(Contexts.sharedInstance.game).AsSingle();
            Container.Bind<CommonStaticData>().FromInstance(_commonStaticData).AsSingle();
            Container.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
            
            if (Application.isMobilePlatform)
                Container.Bind<IScreenTapService>().To<MobileScreenTapService>().AsSingle();
            else
                Container.Bind<IScreenTapService>().To<ComputerScreenTapService>().AsSingle();
            
            Container.BindInterfacesTo<CameraProvider>().AsSingle();
            Container.BindInterfacesTo<RouteFactory>().AsSingle();
            Container.BindInterfacesTo<SoldierFactory>().AsSingle();
            Container.BindInterfacesTo<ProjectileFactory>().AsSingle();
            Container.BindInterfacesTo<LevelFactory>().AsSingle();
            Container.BindInterfacesTo<SaveService>().AsSingle();
            Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
            Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
        }
    }
}