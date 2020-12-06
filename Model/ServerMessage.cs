// <copyright file="ServerMessage.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public abstract class ServerMessage
    {
        public static ServerMessage ReadFrom(System.IO.BinaryReader reader)
        {
            switch (reader.ReadInt32())
            {
                case GetAction.TAG:
                    return GetAction.ReadFrom(reader);
                case Finish.TAG:
                    return Finish.ReadFrom(reader);
                case DebugUpdate.TAG:
                    return DebugUpdate.ReadFrom(reader);
                default:
                    throw new System.Exception("Unexpected tag value");
            }
        }

        public abstract void WriteTo(System.IO.BinaryWriter writer);

        public class GetAction : ServerMessage
        {
            public const int TAG = 0;

            public GetAction(Model.PlayerView playerView, bool debugAvailable)
            {
                this.PlayerView = playerView;
                this.DebugAvailable = debugAvailable;
            }

            public Model.PlayerView PlayerView { get; }

            public bool DebugAvailable { get; }

            public static new GetAction ReadFrom(System.IO.BinaryReader reader)
            {
                PlayerView playerView = Model.PlayerView.ReadFrom(reader);
                bool debugAvailable = reader.ReadBoolean();
                var result = new GetAction(playerView, debugAvailable);
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
                this.PlayerView.WriteTo(writer);
                writer.Write(this.DebugAvailable);
            }
        }

        public class Finish : ServerMessage
        {
            public const int TAG = 1;

            public static new Finish ReadFrom(System.IO.BinaryReader reader)
            {
                var result = new Finish();
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
            }
        }

        public class DebugUpdate : ServerMessage
        {
            public const int TAG = 2;

            public DebugUpdate(Model.PlayerView playerView)
            {
                this.PlayerView = playerView;
            }

            public Model.PlayerView PlayerView { get; set; }

            public static new DebugUpdate ReadFrom(System.IO.BinaryReader reader)
            {
                PlayerView playerView = Model.PlayerView.ReadFrom(reader);
                var result = new DebugUpdate(playerView);
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
                this.PlayerView.WriteTo(writer);
            }
        }
    }
}
