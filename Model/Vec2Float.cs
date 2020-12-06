// <copyright file="Vec2Float.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct Vec2Float
    {
        public Vec2Float(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public static Vec2Float ReadFrom(System.IO.BinaryReader reader)
        {
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            var result = new Vec2Float(x, y);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.X);
            writer.Write(this.Y);
        }
    }
}
