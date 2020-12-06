// <copyright file="BuildProperties.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct BuildProperties
    {
        public BuildProperties(Model.EntityType[] options, int? initHealth)
        {
            this.Options = options;
            this.InitHealth = initHealth;
        }

        public Model.EntityType[] Options { get; set; }

        public int? InitHealth { get; set; }

        public static BuildProperties ReadFrom(System.IO.BinaryReader reader)
        {
            EntityType[] options = new Model.EntityType[reader.ReadInt32()];
            for (int i = 0; i < options.Length; i++)
            {
                switch (reader.ReadInt32())
                {
                    case 0:
                        options[i] = Model.EntityType.Wall;
                        break;
                    case 1:
                        options[i] = Model.EntityType.House;
                        break;
                    case 2:
                        options[i] = Model.EntityType.BuilderBase;
                        break;
                    case 3:
                        options[i] = Model.EntityType.BuilderUnit;
                        break;
                    case 4:
                        options[i] = Model.EntityType.MeleeBase;
                        break;
                    case 5:
                        options[i] = Model.EntityType.MeleeUnit;
                        break;
                    case 6:
                        options[i] = Model.EntityType.RangedBase;
                        break;
                    case 7:
                        options[i] = Model.EntityType.RangedUnit;
                        break;
                    case 8:
                        options[i] = Model.EntityType.Resource;
                        break;
                    case 9:
                        options[i] = Model.EntityType.Turret;
                        break;
                    default:
                        throw new System.Exception("Unexpected tag value");
                }
            }

            int? initHealth = null;
            if (reader.ReadBoolean())
            {
                initHealth = reader.ReadInt32();
            }

            var result = new BuildProperties(options, initHealth);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Options.Length);
            foreach (var optionsElement in this.Options)
            {
                writer.Write((int)optionsElement);
            }

            if (!this.InitHealth.HasValue)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                writer.Write(this.InitHealth.Value);
            }
        }
    }
}
