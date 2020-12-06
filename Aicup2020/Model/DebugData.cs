// <copyright file="DebugData.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public abstract class DebugData
    {
        public static DebugData ReadFrom(System.IO.BinaryReader reader)
        {
            switch (reader.ReadInt32())
            {
                case Log.TAG:
                    return Log.ReadFrom(reader);
                case Primitives.TAG:
                    return Primitives.ReadFrom(reader);
                case PlacedText.TAG:
                    return PlacedText.ReadFrom(reader);
                default:
                    throw new System.Exception("Unexpected tag value");
            }
        }

        public abstract void WriteTo(System.IO.BinaryWriter writer);

        public class Log : DebugData
        {
            public const int TAG = 0;

            public Log(string text)
            {
                this.Text = text;
            }

            public string Text { get; set; }

            public static new Log ReadFrom(System.IO.BinaryReader reader)
            {
                string text = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadInt32()));
                var result = new Log(text);
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
                var textData = System.Text.Encoding.UTF8.GetBytes(this.Text);
                writer.Write(textData.Length);
                writer.Write(textData);
            }
        }

        public class Primitives : DebugData
        {
            public const int TAG = 1;

            public Primitives(Model.ColoredVertex[] vertices, Model.PrimitiveType primitiveType)
            {
                this.Vertices = vertices;
                this.PrimitiveType = primitiveType;
            }

            public Model.ColoredVertex[] Vertices { get; set; }

            public Model.PrimitiveType PrimitiveType { get; set; }

            public static new Primitives ReadFrom(System.IO.BinaryReader reader)
            {
                ColoredVertex[] vertices = new Model.ColoredVertex[reader.ReadInt32()];
                for (int i = 0; i < vertices.Length; i++)
                {
                    vertices[i] = Model.ColoredVertex.ReadFrom(reader);
                }

                Model.PrimitiveType primitiveType;
                switch (reader.ReadInt32())
                {
                    case 0:
                        primitiveType = Model.PrimitiveType.Lines;
                        break;
                    case 1:
                        primitiveType = Model.PrimitiveType.Triangles;
                        break;
                    default:
                        throw new System.Exception("Unexpected tag value");
                }

                var result = new Primitives(vertices, primitiveType);
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
                writer.Write(this.Vertices.Length);
                foreach (var verticesElement in this.Vertices)
                {
                    verticesElement.WriteTo(writer);
                }

                writer.Write((int)this.PrimitiveType);
            }
        }

        public class PlacedText : DebugData
        {
            public const int TAG = 2;

            public PlacedText(Model.ColoredVertex vertex, string text, float alignment, float size)
            {
                this.Vertex = vertex;
                this.Text = text;
                this.Alignment = alignment;
                this.Size = size;
            }

            public Model.ColoredVertex Vertex { get; set; }

            public string Text { get; set; }

            public float Alignment { get; set; }

            public float Size { get; set; }

            public static new PlacedText ReadFrom(System.IO.BinaryReader reader)
            {
                ColoredVertex vertex = Model.ColoredVertex.ReadFrom(reader);
                string text = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadInt32()));
                float alignment = reader.ReadSingle();
                float size = reader.ReadSingle();
                var result = new PlacedText(vertex, text, alignment, size);
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
                this.Vertex.WriteTo(writer);
                var textData = System.Text.Encoding.UTF8.GetBytes(this.Text);
                writer.Write(textData.Length);
                writer.Write(textData);
                writer.Write(this.Alignment);
                writer.Write(this.Size);
            }
        }
    }
}
