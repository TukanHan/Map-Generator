using System;

namespace MapGenerator.UnityPort
{
    public class SpaceOrientationFactory
    {
        public ISpaceOrientation GetSpaceOrientation(SpaceOrientationType orientationType)
        {
            switch (orientationType)
            {
                case SpaceOrientationType.Orientation2D:
                    return new SpaceOrientation2D();
                case SpaceOrientationType.Orientation3D:
                    return new SpaceOrientation3D();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
