using R3;

namespace BlockTower
{
    public interface IActionEventBus
    {
        Observable<ActionEvent> EventStream { get; }
        void Fire(ActionEvent actionEvent);
    }
}