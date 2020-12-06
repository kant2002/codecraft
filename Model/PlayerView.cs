// <copyright file="PlayerView.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct PlayerView
    {
        public PlayerView(int myId, int mapSize, bool fogOfWar, System.Collections.Generic.IDictionary<Model.EntityType, Model.EntityProperties> entityProperties, int maxTickCount, int maxPathfindNodes, int currentTick, Model.Player[] players, Model.Entity[] entities)
        {
            this.MyId = myId;
            this.MapSize = mapSize;
            this.FogOfWar = fogOfWar;
            this.EntityProperties = entityProperties;
            this.MaxTickCount = maxTickCount;
            this.MaxPathfindNodes = maxPathfindNodes;
            this.CurrentTick = currentTick;
            this.Players = players;
            this.Entities = entities;
        }

        public int MyId { get; set; }

        public int MapSize { get; set; }

        public bool FogOfWar { get; set; }

        public System.Collections.Generic.IDictionary<Model.EntityType, Model.EntityProperties> EntityProperties { get; set; }

        public int MaxTickCount { get; set; }

        public int MaxPathfindNodes { get; set; }

        public int CurrentTick { get; set; }

        public Model.Player[] Players { get; set; }

        public Model.Entity[] Entities { get; set; }

        public static PlayerView ReadFrom(System.IO.BinaryReader reader)
        {
            int myId = reader.ReadInt32();
            int mapSize = reader.ReadInt32();
            bool fogOfWar = reader.ReadBoolean();
            int entityPropertiesSize = reader.ReadInt32();
            var entityProperties = new System.Collections.Generic.Dictionary<Model.EntityType, Model.EntityProperties>(entityPropertiesSize);
            for (int i = 0; i < entityPropertiesSize; i++)
            {
                Model.EntityType entityPropertiesKey;
                switch (reader.ReadInt32())
                {
                    case 0:
                        entityPropertiesKey = Model.EntityType.Wall;
                        break;
                    case 1:
                        entityPropertiesKey = Model.EntityType.House;
                        break;
                    case 2:
                        entityPropertiesKey = Model.EntityType.BuilderBase;
                        break;
                    case 3:
                        entityPropertiesKey = Model.EntityType.BuilderUnit;
                        break;
                    case 4:
                        entityPropertiesKey = Model.EntityType.MeleeBase;
                        break;
                    case 5:
                        entityPropertiesKey = Model.EntityType.MeleeUnit;
                        break;
                    case 6:
                        entityPropertiesKey = Model.EntityType.RangedBase;
                        break;
                    case 7:
                        entityPropertiesKey = Model.EntityType.RangedUnit;
                        break;
                    case 8:
                        entityPropertiesKey = Model.EntityType.Resource;
                        break;
                    case 9:
                        entityPropertiesKey = Model.EntityType.Turret;
                        break;
                    default:
                        throw new System.Exception("Unexpected tag value");
                }

                Model.EntityProperties entityPropertiesValue;
                entityPropertiesValue = Model.EntityProperties.ReadFrom(reader);
                entityProperties.Add(entityPropertiesKey, entityPropertiesValue);
            }

            int maxTickCount = reader.ReadInt32();
            int maxPathfindNodes = reader.ReadInt32();
            int currentTick = reader.ReadInt32();
            Player[] players = new Model.Player[reader.ReadInt32()];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = Model.Player.ReadFrom(reader);
            }

            Entity[] entities = new Model.Entity[reader.ReadInt32()];
            for (int i = 0; i < entities.Length; i++)
            {
                entities[i] = Model.Entity.ReadFrom(reader);
            }

            var result = new PlayerView(
                myId,
                mapSize,
                fogOfWar,
                entityProperties,
                maxTickCount,
                maxPathfindNodes,
                currentTick,
                players,
                entities);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.MyId);
            writer.Write(this.MapSize);
            writer.Write(this.FogOfWar);
            writer.Write(this.EntityProperties.Count);
            foreach (var entityPropertiesEntry in this.EntityProperties)
            {
                var entityPropertiesKey = entityPropertiesEntry.Key;
                var entityPropertiesValue = entityPropertiesEntry.Value;
                writer.Write((int)entityPropertiesKey);
                entityPropertiesValue.WriteTo(writer);
            }

            writer.Write(this.MaxTickCount);
            writer.Write(this.MaxPathfindNodes);
            writer.Write(this.CurrentTick);
            writer.Write(this.Players.Length);
            foreach (var playersElement in this.Players)
            {
                playersElement.WriteTo(writer);
            }

            writer.Write(this.Entities.Length);
            foreach (var entitiesElement in this.Entities)
            {
                entitiesElement.WriteTo(writer);
            }
        }
    }
}
