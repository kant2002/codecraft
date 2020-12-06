// <copyright file="RepairProperties.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct RepairProperties
    {
        public RepairProperties(Model.EntityType[] validTargets, int power)
        {
            this.ValidTargets = validTargets;
            this.Power = power;
        }

        public Model.EntityType[] ValidTargets { get; set; }

        public int Power { get; set; }

        public static RepairProperties ReadFrom(System.IO.BinaryReader reader)
        {
            EntityType[] validTargets = new Model.EntityType[reader.ReadInt32()];
            for (int i = 0; i < validTargets.Length; i++)
            {
                switch (reader.ReadInt32())
                {
                    case 0:
                        validTargets[i] = Model.EntityType.Wall;
                        break;
                    case 1:
                        validTargets[i] = Model.EntityType.House;
                        break;
                    case 2:
                        validTargets[i] = Model.EntityType.BuilderBase;
                        break;
                    case 3:
                        validTargets[i] = Model.EntityType.BuilderUnit;
                        break;
                    case 4:
                        validTargets[i] = Model.EntityType.MeleeBase;
                        break;
                    case 5:
                        validTargets[i] = Model.EntityType.MeleeUnit;
                        break;
                    case 6:
                        validTargets[i] = Model.EntityType.RangedBase;
                        break;
                    case 7:
                        validTargets[i] = Model.EntityType.RangedUnit;
                        break;
                    case 8:
                        validTargets[i] = Model.EntityType.Resource;
                        break;
                    case 9:
                        validTargets[i] = Model.EntityType.Turret;
                        break;
                    default:
                        throw new System.Exception("Unexpected tag value");
                }
            }

            int power = reader.ReadInt32();
            var result = new RepairProperties(validTargets, power);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.ValidTargets.Length);
            foreach (var validTargetsElement in this.ValidTargets)
            {
                writer.Write((int)validTargetsElement);
            }

            writer.Write(this.Power);
        }
    }
}
