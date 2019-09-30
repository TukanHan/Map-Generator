using System.Collections.Generic;

namespace MapGenerator.Generator
{
    public class LocationShapeSmoother
    {
        private Stack<Vector2Int> blockPosToCheck = new Stack<Vector2Int>();
        private bool[,] locationShapeMap;
        private readonly LocationsMap locationsMap;

        public LocationShapeSmoother(LocationsMap locationsMap)
        {
            this.locationsMap = locationsMap;
        }

        public List<Vector2Int> SmoothLocationShape(bool[,] locationShapeMap)
        {
            this.locationShapeMap = locationShapeMap;

            for(int i=0; i<locationsMap.Height; i++)
            {
                for (int j = 0; j < locationsMap.Width; j++)
                {
                    blockPosToCheck.Push(new Vector2Int(j, i));
                }
            }

            int loopCount = locationsMap.Width * locationsMap.Height * 10;
            while (blockPosToCheck.Count > 0)
            {
                SmoothBlock(blockPosToCheck.Pop());

                if (--loopCount < 0)
                    throw new System.Exception("Infinit loop");
            }

            return SelectBlockFromArray();
        }

        private List<Vector2Int> SelectBlockFromArray()
        {
            List<Vector2Int> selectedBlocks = new List<Vector2Int>();

            for (int i = 0; i < locationsMap.Height; ++i)
            {
                for (int j = 0; j < locationsMap.Width; ++j)
                {
                    if (locationShapeMap[i, j])
                        selectedBlocks.Add(new Vector2Int(j, i));
                }
            }

            return selectedBlocks;
        }

        private void SmoothBlock(Vector2Int pos)
        {
            List<Vector2Int> neighbourPos = new List<Vector2Int>();
            int countOfNeighbourBlock = 0;
            int countOfNeighbourEmpty = 0;
            int countOfNeighbour = 0;

            for (int i = pos.Y - 1; i <= pos.Y + 1; ++i)
            {
                for (int j = pos.X - 1; j <= pos.X + 1; ++j)
                {
                    if (i != pos.Y || j != pos.X)
                    {
                        if (locationsMap.IsOnMap(new Vector2Int(j, i)))
                        {
                            if (locationShapeMap[i, j])
                            {
                                countOfNeighbourBlock++;
                                if ((i - pos.Y + 1 == j - pos.X) || (j - pos.X + 1 == i - pos.Y))
                                    countOfNeighbour++;
                            }
                            else
                            {
                                countOfNeighbourEmpty++;
                            }

                            neighbourPos.Add(new Vector2Int(j, i));
                        }
                    }
                }
            }

            if (countOfNeighbourEmpty == 5 && locationShapeMap[pos.Y, pos.X]) //Delete corner block
            {
                locationShapeMap[pos.Y, pos.X] = false;
                neighbourPos.ForEach(x => blockPosToCheck.Push(x));
            }
            else if (countOfNeighbourBlock == 5 && !locationShapeMap[pos.Y, pos.X]) //Add corner block 
            {
                locationShapeMap[pos.Y, pos.X] = true;
                neighbourPos.ForEach(x => blockPosToCheck.Push(x));
            }
            else if (countOfNeighbour < 2 && locationShapeMap[pos.Y, pos.X])
            {
                locationShapeMap[pos.Y, pos.X] = false;
                neighbourPos.ForEach(x => blockPosToCheck.Push(x));
            }
            else if (countOfNeighbour > 2 && !locationShapeMap[pos.Y, pos.X] && locationsMap.CanGenerateIn(pos))
            {
                locationShapeMap[pos.Y, pos.X] = true;
                neighbourPos.ForEach(x => blockPosToCheck.Push(x));
            }
        }
    }
}