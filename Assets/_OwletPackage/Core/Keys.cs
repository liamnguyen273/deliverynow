using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Owlet
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

        public static partial class SerializableObject
        {
            public static partial class Tags
            {
                public static readonly string Transform = "transform";
                public static readonly string Player = "player";
                public static readonly string Spline = "spline";
            }
        }

        public static partial class Prefabs
        {
            public static readonly string BaseMap = nameof(BaseMap);
        }
    }
}