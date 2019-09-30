using MapGenerator.Generator;
using System;

namespace MapGenerator.DataModels
{
    public class FenceModel
    {
        public AbstractObjectModel TopLeft { get; set; }
        public AbstractObjectModel TopLeftInside { get; set; }
        public AbstractObjectModel TopMiddle { get; set; }
        public AbstractObjectModel TopRight { get; set; }
        public AbstractObjectModel TopRightInside { get; set; }

        public AbstractObjectModel MiddleLeft { get; set; }
        public AbstractObjectModel MiddleRight { get; set; }

        public AbstractObjectModel BottomLeft { get; set; }
        public AbstractObjectModel BottomLeftInside { get; set; }
        public AbstractObjectModel BottomMiddle { get; set; }
        public AbstractObjectModel BottomRight { get; set; }
        public AbstractObjectModel BottomRightInside { get; set; }

        private static readonly bool?[,] topLeftCondition = new bool?[,] { { null, true, null }, { false, null, true }, { null, false, null } };
        private static readonly bool?[,] topRightCondition = new bool?[,] { { null, true, null }, { true, null, false }, { null, false, null } };
        private static readonly bool?[,] bottomRightCondition = new bool?[,] { { null, false, null }, { true, null, false }, { null, true, null } };
        private static readonly bool?[,] bottomLeftCondition = new bool?[,] { { null, false, null }, { false, null, true }, { null, true, null } };
        private static readonly bool?[,] middleLeftCondition = new bool?[,] { { null, null, null }, { false, null, true }, { null, null, null } };
        private static readonly bool?[,] middleRightCondition = new bool?[,] { { null, null, null }, { true, null, false }, { null, null, null } };
        private static readonly bool?[,] topMiddleCondition = new bool?[,] { { null, true, null }, { null, null, null }, { null, false, null } };
        private static readonly bool?[,] bottomMiddleCondition = new bool?[,] { { null, false, null }, { null, null, null }, { null, true, null } };
        private static readonly bool?[,] bottomRightInsideCondition = new bool?[,] { { null, true, false }, { null, null, true }, { null, null, null } };
        private static readonly bool?[,] bottomLeftInsideCondition = new bool?[,] { { false, true, null }, { true, null, null }, { null, null, null } };
        private static readonly bool?[,] topLeftInsideCondition = new bool?[,] { { null, null, null }, { true, null, null }, { false, true, null } };
        private static readonly bool?[,] topRightInsideCondition = new bool?[,] { { null, null, null }, { null, null, true }, { null, true, false } };


        public AbstractObjectModel GetFancePart(LocationTileType[,] neighborhoodStateMap)
        {
            if (ValidateTemplate(bottomRightInsideCondition, neighborhoodStateMap))
                return BottomRightInside;

            else if (ValidateTemplate(topLeftCondition, neighborhoodStateMap))
                return TopLeft;

            else if (ValidateTemplate(bottomLeftInsideCondition, neighborhoodStateMap))
                return BottomLeftInside;

            else if (ValidateTemplate(topRightCondition, neighborhoodStateMap))
                return TopRight;

            else if (ValidateTemplate(topLeftInsideCondition, neighborhoodStateMap))
                return TopLeftInside;

            else if (ValidateTemplate(bottomRightCondition, neighborhoodStateMap))
                return BottomRight;

            else if (ValidateTemplate(topRightInsideCondition, neighborhoodStateMap))
                return TopRightInside;

            else if (ValidateTemplate(bottomLeftCondition, neighborhoodStateMap))
                return BottomLeft;

            else if (ValidateTemplate(middleLeftCondition, neighborhoodStateMap))
                return MiddleLeft;

            else if (ValidateTemplate(middleRightCondition, neighborhoodStateMap))
                return MiddleRight;

            else if (ValidateTemplate(topMiddleCondition, neighborhoodStateMap))
                return TopMiddle;

            else if (ValidateTemplate(bottomMiddleCondition, neighborhoodStateMap))
                return BottomMiddle;

            return null;
        }

        private bool ValidateTemplate(bool?[,] template, LocationTileType[,] map)
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (template[i, j].HasValue)
                    {
                        if (template[i, j].Value != ToBool(map[i, j]))
                            return false;
                    }
                }
            }

            return true;
        }

        private bool ToBool(LocationTileType value)
        {
            switch(value)
            {
                case LocationTileType.Border:
                case LocationTileType.Space:
                    return true;
                case LocationTileType.None:
                    return false;
                default:
                    throw new InvalidOperationException(value.ToString());
            }
        }
    }
}
