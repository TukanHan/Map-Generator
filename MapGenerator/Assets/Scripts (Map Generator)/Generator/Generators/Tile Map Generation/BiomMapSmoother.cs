using System.Linq;
using System.Collections.Generic;
using MapGenerator.DataModels;

namespace MapGenerator.Generator
{
    public class BiomMapSmoother
    {
        private TilesMap map;

        private Stack<Vector2Int> positionToCheck = new Stack<Vector2Int>();

        public void SmoothBiomMap(TilesMap map)
        {
            this.map = map;

            for (int i = 0; i < map.Height; ++i)
            {
                for (int j = 0; j < map.Width; ++j)
                {
                    positionToCheck.Push(new Vector2Int(j, i));
                }
            }

            int loopCount = map.Width * map.Height * 2;
            while (positionToCheck.Count > 0)
            {
                SmoothBiom(positionToCheck.Pop());

                if (--loopCount < 0)
                    break;
            }
        }

        private void SmoothBiom(Vector2Int pos)
        {
            int countOfSameBiom = 0;
            List<Vector2Int> neighbours = new List<Vector2Int>();
            Vector2Int last;

            if (map.IsOnMap(last = new Vector2Int(pos.X - 1, pos.Y)))
            {
                if (map[pos.Y, pos.X].Biom == map[pos.Y, pos.X - 1].Biom)
                    countOfSameBiom++;

                neighbours.Add(last);
            }

            if (map.IsOnMap(last = new Vector2Int(pos.X, pos.Y - 1)))
            {
                if (map[pos.Y, pos.X].Biom == map[pos.Y - 1, pos.X].Biom)
                    countOfSameBiom++;

                neighbours.Add(last);
            }

            if (map.IsOnMap(last = new Vector2Int(pos.X + 1, pos.Y)))
            {
                if (map[pos.Y, pos.X].Biom == map[pos.Y, pos.X + 1].Biom)
                    countOfSameBiom++;

                neighbours.Add(last);
            }

            if (map.IsOnMap(last = new Vector2Int(pos.X, pos.Y + 1)))
            {
                if (map[pos.Y, pos.X].Biom == map[pos.Y + 1, pos.X].Biom)
                    countOfSameBiom++;

                neighbours.Add(last);
            }

            if (countOfSameBiom < 2)
            {
                BiomModel mostFrequentBiom = neighbours.GroupBy(x => map[x.Y, x.X].Biom)
                    .Select(x => new { biom = x, cnt = x.Count() })
                    .OrderByDescending(g => g.cnt)
                    .Select(g => g.biom).First().Key;

                map[pos.Y, pos.X].Biom = mostFrequentBiom;
                neighbours.ForEach(x => positionToCheck.Push(x));
            }
        }
    }
}