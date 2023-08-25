using UnityEngine;
using Zenject;

public class RevealInstaller : MonoInstaller
{
    [SerializeField] private Reveal.RevealObjectPool pool;
    
    public override void InstallBindings() => Container.Bind<Reveal.RevealObjectPool>().FromInstance(pool).AsSingle();
}
