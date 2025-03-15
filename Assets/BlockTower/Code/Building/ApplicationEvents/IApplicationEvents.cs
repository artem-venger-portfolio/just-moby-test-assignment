using System;

namespace BlockTower.Building.Update
{
    public interface IApplicationEvents
    {
        event Action Updated;
    }
}