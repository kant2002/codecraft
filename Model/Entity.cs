// <copyright file="Entity.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct Entity
    {
        public Entity(int id, int? playerId, Model.EntityType entityType, Model.Vec2Int position, int health, bool active)
        {
            this.Id = id;
            this.PlayerId = playerId;
            this.EntityType = entityType;
            this.Position = position;
            this.Health = health;
            this.Active = active;
        }

        public int Id { get; set; }

        public int? PlayerId { get; set; }

        public Model.EntityType EntityType { get; set; }

        public Model.Vec2Int Position { get; set; }

        public int Health { get; set; }

        public bool Active { get; set; }

        public static Entity ReadFrom(System.IO.BinaryReader reader)
        {
            int id = reader.ReadInt32();
            int? playerId = null;
            if (reader.ReadBoolean())
            {
                playerId = reader.ReadInt32();
            }

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
            int health = reader.ReadInt32();
            bool active = reader.ReadBoolean();
            var result = new Entity(id, playerId, entityType, position, health, active);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Id);
            if (!this.PlayerId.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                writer.Write(this.PlayerId.Value);
            }

            writer.Write((int)this.EntityType);
            this.Position.WriteTo(writer);
            writer.Write(this.Health);
            writer.Write(this.Active);
        }
    }
}
