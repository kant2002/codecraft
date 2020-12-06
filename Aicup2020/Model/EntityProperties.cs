// <copyright file="EntityProperties.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct EntityProperties
    {
        public EntityProperties(int size, int buildScore, int destroyScore, bool canMove, int populationProvide, int populationUse, int maxHealth, int initialCost, int sightRange, int resourcePerHealth, Model.BuildProperties? build, Model.AttackProperties? attack, Model.RepairProperties? repair)
        {
            this.Size = size;
            this.BuildScore = buildScore;
            this.DestroyScore = destroyScore;
            this.CanMove = canMove;
            this.PopulationProvide = populationProvide;
            this.PopulationUse = populationUse;
            this.MaxHealth = maxHealth;
            this.InitialCost = initialCost;
            this.SightRange = sightRange;
            this.ResourcePerHealth = resourcePerHealth;
            this.Build = build;
            this.Attack = attack;
            this.Repair = repair;
        }

        public int Size { get; set; }

        public int BuildScore { get; set; }

        public int DestroyScore { get; set; }

        public bool CanMove { get; set; }

        public int PopulationProvide { get; set; }

        public int PopulationUse { get; set; }

        public int MaxHealth { get; set; }

        public int InitialCost { get; set; }

        public int SightRange { get; set; }

        public int ResourcePerHealth { get; set; }

        public Model.BuildProperties? Build { get; set; }

        public Model.AttackProperties? Attack { get; set; }

        public Model.RepairProperties? Repair { get; set; }

        public static EntityProperties ReadFrom(System.IO.BinaryReader reader)
        {
            int size = reader.ReadInt32();
            int buildScore = reader.ReadInt32();
            int destroyScore = reader.ReadInt32();
            bool canMove = reader.ReadBoolean();
            int populationProvide = reader.ReadInt32();
            int populationUse = reader.ReadInt32();
            int maxHealth = reader.ReadInt32();
            int initialCost = reader.ReadInt32();
            int sightRange = reader.ReadInt32();
            int resourcePerHealth = reader.ReadInt32();
            Model.BuildProperties? build = null;
            if (reader.ReadBoolean())
            {
                build = Model.BuildProperties.ReadFrom(reader);
            }

            Model.AttackProperties? attack = null;
            if (reader.ReadBoolean())
            {
                attack = Model.AttackProperties.ReadFrom(reader);
            }

            Model.RepairProperties? repair = null;
            if (reader.ReadBoolean())
            {
                repair = Model.RepairProperties.ReadFrom(reader);
            }

            var result = new EntityProperties(
                size,
                buildScore,
                destroyScore,
                canMove,
                populationProvide,
                populationUse,
                maxHealth,
                initialCost,
                sightRange,
                resourcePerHealth,
                build,
                attack,
                repair);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Size);
            writer.Write(this.BuildScore);
            writer.Write(this.DestroyScore);
            writer.Write(this.CanMove);
            writer.Write(this.PopulationProvide);
            writer.Write(this.PopulationUse);
            writer.Write(this.MaxHealth);
            writer.Write(this.InitialCost);
            writer.Write(this.SightRange);
            writer.Write(this.ResourcePerHealth);
            if (!this.Build.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                this.Build.Value.WriteTo(writer);
            }

            if (!this.Attack.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                this.Attack.Value.WriteTo(writer);
            }

            if (!this.Repair.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                this.Repair.Value.WriteTo(writer);
            }
        }
    }
}
