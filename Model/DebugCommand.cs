// <copyright file="DebugCommand.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public abstract class DebugCommand
    {
        public static DebugCommand ReadFrom(System.IO.BinaryReader reader)
        {
            switch (reader.ReadInt32())
            {
                case Add.TAG:
                    return Add.ReadFrom(reader);
                case Clear.TAG:
                    return Clear.ReadFrom(reader);
                default:
                    throw new System.Exception("Unexpected tag value");
            }
        }

        public abstract void WriteTo(System.IO.BinaryWriter writer);

        public class Add : DebugCommand
        {
            public const int TAG = 0;

            public Add(Model.DebugData data)
            {
                this.Data = data;
            }

            public Model.DebugData Data { get; set; }

            public static new Add ReadFrom(System.IO.BinaryReader reader)
            {
                DebugData debugData = Model.DebugData.ReadFrom(reader);
                var result = new Add(debugData);
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
                this.Data.WriteTo(writer);
            }
        }

        public class Clear : DebugCommand
        {
            public const int TAG = 1;

            public static new Clear ReadFrom(System.IO.BinaryReader reader)
            {
                var result = new Clear();
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
            }
        }
    }
}
