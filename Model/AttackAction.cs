// <copyright file="AttackAction.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct AttackAction
    {
        public AttackAction(int? target, Model.AutoAttack? autoAttack)
        {
            this.Target = target;
            this.AutoAttack = autoAttack;
        }

        public int? Target { get; set; }

        public Model.AutoAttack? AutoAttack { get; set; }

        public static AttackAction ReadFrom(System.IO.BinaryReader reader)
        {
            int? target = null;
            if (reader.ReadBoolean())
            {
                target = reader.ReadInt32();
            }

            Model.AutoAttack? autoAttack = null;
            if (reader.ReadBoolean())
            {
                autoAttack = Model.AutoAttack.ReadFrom(reader);
            }

            var result = new AttackAction(target, autoAttack);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            if (!this.Target.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                writer.Write(this.Target.Value);
            }

            if (!this.AutoAttack.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                this.AutoAttack.Value.WriteTo(writer);
            }
        }
    }
}
