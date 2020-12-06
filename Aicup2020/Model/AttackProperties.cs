// <copyright file="AttackProperties.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct AttackProperties
    {
        public AttackProperties(int attackRange, int damage, bool collectResource)
        {
            this.AttackRange = attackRange;
            this.Damage = damage;
            this.CollectResource = collectResource;
        }

        public int AttackRange { get; set; }

        public int Damage { get; set; }

        public bool CollectResource { get; set; }

        public static AttackProperties ReadFrom(System.IO.BinaryReader reader)
        {
            int attackRange = reader.ReadInt32();
            int damage = reader.ReadInt32();
            bool collectResource = reader.ReadBoolean();
            var result = new AttackProperties(attackRange, damage, collectResource);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.AttackRange);
            writer.Write(this.Damage);
            writer.Write(this.CollectResource);
        }
    }
}
