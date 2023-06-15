using Code.Gameplay.Entities.Stats;
using UnityEngine;

namespace Code.Gameplay.Entities
{
    public interface IEntityFacade
    {
        IStatsProcessor StatsProcessor { get; }
        Vector2 Position { get; }
    }
}