// <copyright file="EntityAction.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct EntityAction
    {
        public EntityAction(Model.MoveAction? moveAction, Model.BuildAction? buildAction, Model.AttackAction? attackAction, Model.RepairAction? repairAction)
        {
            this.MoveAction = moveAction;
            this.BuildAction = buildAction;
            this.AttackAction = attackAction;
            this.RepairAction = repairAction;
        }

        public Model.MoveAction? MoveAction { get; set; }

        public Model.BuildAction? BuildAction { get; set; }

        public Model.AttackAction? AttackAction { get; set; }

        public Model.RepairAction? RepairAction { get; set; }

        public static EntityAction ReadFrom(System.IO.BinaryReader reader)
        {
            Model.MoveAction? moveAction = null;
            if (reader.ReadBoolean())
            {
                moveAction = Model.MoveAction.ReadFrom(reader);
            }

            Model.BuildAction? buildAction = null;
            if (reader.ReadBoolean())
            {
                buildAction = Model.BuildAction.ReadFrom(reader);
            }

            Model.AttackAction? attackAction = null;
            if (reader.ReadBoolean())
            {
                attackAction = Model.AttackAction.ReadFrom(reader);
            }

            Model.RepairAction? repairAction = null;
            if (reader.ReadBoolean())
            {
                repairAction = Model.RepairAction.ReadFrom(reader);
            }

            var result = new EntityAction(moveAction, buildAction, attackAction, repairAction);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            if (!this.MoveAction.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                this.MoveAction.Value.WriteTo(writer);
            }

            if (!this.BuildAction.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                this.BuildAction.Value.WriteTo(writer);
            }

            if (!this.AttackAction.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                this.AttackAction.Value.WriteTo(writer);
            }

            if (!this.RepairAction.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                this.RepairAction.Value.WriteTo(writer);
            }
        }
    }
}
