// <copyright file="DebugInterface.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020
{
    using System.IO;

    public class DebugInterface
    {
        private BinaryWriter writer;
        private BinaryReader reader;

        public DebugInterface(BinaryReader reader, BinaryWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Send(Model.DebugCommand command)
        {
            new Model.ClientMessage.DebugMessage(command).WriteTo(this.writer);
            this.writer.Flush();
        }

        public Model.DebugState GetState()
        {
            new Model.ClientMessage.RequestDebugState().WriteTo(this.writer);
            this.writer.Flush();
            return Model.DebugState.ReadFrom(this.reader);
        }
    }
}