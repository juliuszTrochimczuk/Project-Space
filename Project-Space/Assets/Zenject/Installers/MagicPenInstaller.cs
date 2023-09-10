using UnityEngine;
using MagicPen;
using Zenject;

namespace Installer
{
    public class MagicPenInstaller : MonoInstaller
    {
        [SerializeField] private MagicObjectsPool objectsPool;
        [SerializeField] private PlayerMagicPen magicPen;
        [SerializeField] private MagicPenUI ui;

        public override void InstallBindings()
        {
            Container.Bind<MagicObjectsPool>().FromInstance(objectsPool).AsSingle();
            Container.Bind<PlayerMagicPen>().FromInstance(magicPen).AsSingle();
            Container.Bind<MagicPenUI>().FromInstance(ui).AsSingle();
        }
    }
}