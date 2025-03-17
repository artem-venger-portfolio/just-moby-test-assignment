using JetBrains.Annotations;
using R3;

namespace BlockTower
{
    [UsedImplicitly]
    public class ActionEventBus : IActionEventBus
    {
        private readonly Subject<ActionEvent> _eventStream = new();

        public Observable<ActionEvent> EventStream => _eventStream;

        public void Fire(ActionEvent actionEvent)
        {
            _eventStream.OnNext(actionEvent);
        }
    }
}