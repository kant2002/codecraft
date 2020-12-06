// <copyright file="Action.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct Action
    {
        public Action(System.Collections.Generic.IDictionary<int, Model.EntityAction> entityActions)
        {
            this.EntityActions = entityActions;
        }

        public System.Collections.Generic.IDictionary<int, Model.EntityAction> EntityActions { get; set; }

        public static Action ReadFrom(System.IO.BinaryReader reader)
        {
            int entityActionsSize = reader.ReadInt32();
            var entityActions = new System.Collections.Generic.Dictionary<int, Model.EntityAction>(entityActionsSize);
            for (int i = 0; i < entityActionsSize; i++)
            {
                int entityActionsKey;
                entityActionsKey = reader.ReadInt32();
                Model.EntityAction entityActionsValue;
                entityActionsValue = Model.EntityAction.ReadFrom(reader);
                entityActions.Add(entityActionsKey, entityActionsValue);
            }

            var result = new Action(entityActions);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            writer.Write(this.EntityActions.Count);
            foreach (var entityActionsEntry in this.EntityActions)
            {
                var entityActionsKey = entityActionsEntry.Key;
                var entityActionsValue = entityActionsEntry.Value;
                writer.Write(entityActionsKey);
                entityActionsValue.WriteTo(writer);
            }
        }
    }
}
