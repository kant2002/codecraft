// <copyright file="ColoredVertex.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct ColoredVertex
    {
        public ColoredVertex(Model.Vec2Float? worldPos, Model.Vec2Float screenOffset, Model.Color color)
        {
            this.WorldPos = worldPos;
            this.ScreenOffset = screenOffset;
            this.Color = color;
        }

        public Model.Vec2Float? WorldPos { get; set; }

        public Model.Vec2Float ScreenOffset { get; set; }

        public Model.Color Color { get; set; }

        public static ColoredVertex ReadFrom(System.IO.BinaryReader reader)
        {
            Model.Vec2Float? worldPos = null;
            if (reader.ReadBoolean())
            {
                worldPos = Model.Vec2Float.ReadFrom(reader);
            }

            Vec2Float screenOffset = Model.Vec2Float.ReadFrom(reader);
            Color color = Model.Color.ReadFrom(reader);
            var result = new ColoredVertex(worldPos, screenOffset, color);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            if (!this.WorldPos.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                this.WorldPos.Value.WriteTo(writer);
            }

            this.ScreenOffset.WriteTo(writer);
            this.Color.WriteTo(writer);
        }
    }
}
