using MapGenerator.DataModels;
using UnityEngine;

namespace MapGenerator.UnityPort
{
    public interface IGraphicalMapGenerator
    {
        void Render(Transform parentTransform, TilesMap map);
    }
}
