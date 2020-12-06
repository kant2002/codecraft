// <copyright file="RepairAction.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct RepairAction
    {
        public RepairAction(int target)
        {
            this.Target = target;
        }

        public int Target { get; }

        public static RepairAction ReadFrom(System.IO.BinaryReader reader)
        {
            int target = reader.ReadInt32();
            var result = new RepairAction(target);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Target);
        }
    }
}
