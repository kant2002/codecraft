// <copyright file="BuildAction.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct BuildAction
    {
        public BuildAction(Model.EntityType entityType, Model.Vec2Int position)
        {
            this.EntityType = entityType;
            this.Position = position;
        }

        public Model.EntityType EntityType { get; set; }

        public Model.Vec2Int Position { get; set; }

        public static BuildAction ReadFrom(System.IO.BinaryReader reader)
        {
            Model.EntityType entityType;
            switch (reader.ReadInt32())
            {
                case 0:
                    entityType = Model.EntityType.Wall;
                    break;
                case 1:
                    entityType = Model.EntityType.House;
                    break;
                case 2:
                    entityType = Model.EntityType.BuilderBase;
                    break;
                case 3:
                    entityType = Model.EntityType.BuilderUnit;
                    break;
                case 4:
                    entityType = Model.EntityType.MeleeBase;
                    break;
                case 5:
                    entityType = Model.EntityType.MeleeUnit;
                    break;
                case 6:
                    entityType = Model.EntityType.RangedBase;
                    break;
                case 7:
                    entityType = Model.EntityType.RangedUnit;
                    break;
                case 8:
                    entityType = Model.EntityType.Resource;
                    break;
                case 9:
                    entityType = Model.EntityType.Turret;
                    break;
                default:
                    throw new System.Exception("Unexpected tag value");
            }

            Vec2Int position = Model.Vec2Int.ReadFrom(reader);
            var result = new BuildAction(entityType, position);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write((int)this.EntityType);
            this.Position.WriteTo(writer);
        }
    }
}
