using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public static partial class Keys
    {
        public static partial class Delims
        {
            public static partial class SerializableObject
            {
                public static readonly string Name = "::";
                public static readonly string Content = "~~";
                public static readonly string Title = "__";
                public static readonly string Data = "||";
                public static readonly string Split = "..";

                public static partial class Tags
                {
                    public static readonly string Transform = "transform";
                }
            }
        }

        public static class Data
        {
            public static int AvailableLevel = 10;
        }

        public static partial class SerializableObject
        {
            public static partial class Tags
            {
                public static readonly string Transform = "transform";
                public static readonly string Player = "player";
                public static readonly string Spline = "spline";
            }
        }

        public static partial class Addressables
        {
            public static readonly string BaseMap = nameof(BaseMap);
            public static readonly string Player = nameof(Player);
            public static readonly string Path = nameof(Path);
        }

        public static class Popups
        {
            public static readonly string TapToStart = nameof(TapToStart);
            public static readonly string LevelComplete = nameof(LevelComplete);
        }
    }
}