// <copyright file="Player.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct Player
    {
        public Player(int id, int score, int resource)
        {
            this.Id = id;
            this.Score = score;
            this.Resource = resource;
        }

        public int Id { get; set; }

        public int Score { get; set; }

        public int Resource { get; set; }

        public static Player ReadFrom(System.IO.BinaryReader reader)
        {
            int id = reader.ReadInt32();
            int score = reader.ReadInt32();
            int resource = reader.ReadInt32();
            var result = new Player(id, score, resource);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Id);
            writer.Write(this.Score);
            writer.Write(this.Resource);
        }
    }
}
