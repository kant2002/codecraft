// <copyright file="Camera.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct Camera
    {
        public Camera(Model.Vec2Float center, float rotation, float attack, float distance, bool perspective)
        {
            this.Center = center;
            this.Rotation = rotation;
            this.Attack = attack;
            this.Distance = distance;
            this.Perspective = perspective;
        }

        public Model.Vec2Float Center { get; set; }

        public float Rotation { get; set; }

        public float Attack { get; set; }

        public float Distance { get; set; }

        public bool Perspective { get; set; }

        public static Camera ReadFrom(System.IO.BinaryReader reader)
        {
            Vec2Float center = Model.Vec2Float.ReadFrom(reader);
            float rotation = reader.ReadSingle();
            float attack = reader.ReadSingle();
            float distance = reader.ReadSingle();
            bool perspective = reader.ReadBoolean();
            var result = new Camera(center, rotation, attack, distance, perspective);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            this.Center.WriteTo(writer);
            writer.Write(this.Rotation);
            writer.Write(this.Attack);
            writer.Write(this.Distance);
            writer.Write(this.Perspective);
        }
    }
}
