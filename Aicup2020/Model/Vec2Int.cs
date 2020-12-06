// <copyright file="Vec2Int.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct Vec2Int
    {
        public Vec2Int(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public static Vec2Int ReadFrom(System.IO.BinaryReader reader)
        {
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();
            var result = new Vec2Int(x, y);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.X);
            writer.Write(this.Y);
        }
    }
}
