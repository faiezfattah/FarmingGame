using Script.Feature.Character.Player;
using Script.Feature.Input;
using R3;
using System;
public class PlayerInputBridge : IDisposable {
    IDisposable disposeable;
    public PlayerInputBridge(
        InputProcessor inputProcessor,
        PlayerController player

    ) {
        var builder = new DisposableBuilder();

        inputProcessor.MoveEvent.Subscribe(player.UpdateMoveDir).AddTo(ref builder);
        inputProcessor.InteractEvent.Subscribe(_ => player.Interact()).AddTo(ref builder);
        

        disposeable = builder.Build();
    }

    public void Dispose() {
        disposeable?.Dispose();
    }
}