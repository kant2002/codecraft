// <copyright file="Color.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct Color
    {
        public Color(float r, float g, float b, float a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public float R { get; set; }

        public float G { get; set; }

        public float B { get; set; }

        public float A { get; set; }

        public static Color ReadFrom(System.IO.BinaryReader reader)
        {
            float r = reader.ReadSingle();
            float g = reader.ReadSingle();
            float b = reader.ReadSingle();
            float a = reader.ReadSingle();
            var result = new Color(r, g, b, a);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.R);
            writer.Write(this.G);
            writer.Write(this.B);
            writer.Write(this.A);
        }
    }
}
