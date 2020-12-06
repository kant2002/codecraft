// <copyright file="AutoAttack.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct AutoAttack
    {
        public AutoAttack(int pathfindRange, Model.EntityType[] validTargets)
        {
            this.PathfindRange = pathfindRange;
            this.ValidTargets = validTargets;
        }

        public int PathfindRange { get; set; }

        public Model.EntityType[] ValidTargets { get; set; }

        public static AutoAttack ReadFrom(System.IO.BinaryReader reader)
        {
            int pathfindRange = reader.ReadInt32();
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

            var result = new AutoAttack(pathfindRange, validTargets);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.PathfindRange);
            writer.Write(this.ValidTargets.Length);
            foreach (var validTargetsElement in this.ValidTargets)
            {
                writer.Write((int)validTargetsElement);
            }
        }
    }
}
