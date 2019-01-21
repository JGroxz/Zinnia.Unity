﻿using VRTK.Core.Tracking.Collision;
using VRTK.Core.Tracking.Collision.Active;

namespace Test.VRTK.Core.Utility.Helper
{
    public static class ActiveCollisionsHelper
    {
        public static string GetNamesOfActiveCollisions(ActiveCollisionsContainer.EventData list, string separator = ",")
        {
            string returnString = "";
            for (int index = 0; index < list.activeCollisions.Count; index++)
            {
                CollisionNotifier.EventData data = list.activeCollisions[index];
                returnString += data.collider.name;
                if (index < list.activeCollisions.Count - 1)
                {
                    returnString += separator;
                }
            }
            return returnString;
        }
    }
}