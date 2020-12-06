// <copyright file="ClientMessage.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public abstract class ClientMessage
    {
        public static ClientMessage ReadFrom(System.IO.BinaryReader reader)
        {
            switch (reader.ReadInt32())
            {
                case DebugMessage.TAG:
                    return DebugMessage.ReadFrom(reader);
                case ActionMessage.TAG:
                    return ActionMessage.ReadFrom(reader);
                case DebugUpdateDone.TAG:
                    return DebugUpdateDone.ReadFrom(reader);
                case RequestDebugState.TAG:
                    return RequestDebugState.ReadFrom(reader);
                default:
                    throw new System.Exception("Unexpected tag value");
            }
        }

        public abstract void WriteTo(System.IO.BinaryWriter writer);

        public class DebugMessage : ClientMessage
        {
            public const int TAG = 0;

            public DebugMessage(Model.DebugCommand command)
            {
                this.Command = command;
            }

            public Model.DebugCommand Command { get; set; }

            public static new DebugMessage ReadFrom(System.IO.BinaryReader reader)
            {
                DebugCommand debugCommand = Model.DebugCommand.ReadFrom(reader);
                var result = new DebugMessage(debugCommand);
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
                this.Command.WriteTo(writer);
            }
        }

        public class ActionMessage : ClientMessage
        {
            public const int TAG = 1;

            public ActionMessage(Model.Action action)
            {
                this.Action = action;
            }

            public Model.Action Action { get; set; }

            public static new ActionMessage ReadFrom(System.IO.BinaryReader reader)
            {
                Action action = Model.Action.ReadFrom(reader);
                var result = new ActionMessage(action);
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
                this.Action.WriteTo(writer);
            }
        }

        public class DebugUpdateDone : ClientMessage
        {
            public const int TAG = 2;

            public static new DebugUpdateDone ReadFrom(System.IO.BinaryReader reader)
            {
                var result = new DebugUpdateDone();
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
            }
        }

        public class RequestDebugState : ClientMessage
        {
            public const int TAG = 3;

            public static new RequestDebugState ReadFrom(System.IO.BinaryReader reader)
            {
                var result = new RequestDebugState();
                return result;
            }

            public override void WriteTo(System.IO.BinaryWriter writer)
            {
                writer.Write(TAG);
            }
        }
    }
}
