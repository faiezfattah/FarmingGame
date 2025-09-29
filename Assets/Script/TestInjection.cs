using Reflex.Core;
using UnityEngine;

public class TestInjector : MonoBehaviour, IInstaller {
    public void InstallBindings(ContainerBuilder containerBuilder) {
        containerBuilder.AddSingleton("Hello world");
    }
}