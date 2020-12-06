// <copyright file="MoveAction.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct MoveAction
    {
        public MoveAction(Model.Vec2Int target, bool findClosestPosition, bool breakThrough)
        {
            this.Target = target;
            this.FindClosestPosition = findClosestPosition;
            this.BreakThrough = breakThrough;
        }

        public Model.Vec2Int Target { get; set; }

        public bool FindClosestPosition { get; set; }

        public bool BreakThrough { get; set; }

        public static MoveAction ReadFrom(System.IO.BinaryReader reader)
        {
            Vec2Int target = Model.Vec2Int.ReadFrom(reader);
            bool findClosestPosition = reader.ReadBoolean();
            bool breakThrough = reader.ReadBoolean();
            var result = new MoveAction(target, findClosestPosition, breakThrough);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            this.Target.WriteTo(writer);
            writer.Write(this.FindClosestPosition);
            writer.Write(this.BreakThrough);
        }
    }
}
