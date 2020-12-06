// <copyright file="DebugState.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020.Model
{
    public struct DebugState
    {
        public DebugState(Model.Vec2Int windowSize, Model.Vec2Float mousePosWindow, Model.Vec2Float mousePosWorld, string[] pressedKeys, Model.Camera camera, int playerIndex)
        {
            this.WindowSize = windowSize;
            this.MousePosWindow = mousePosWindow;
            this.MousePosWorld = mousePosWorld;
            this.PressedKeys = pressedKeys;
            this.Camera = camera;
            this.PlayerIndex = playerIndex;
        }

        public Model.Vec2Int WindowSize { get; set; }

        public Model.Vec2Float MousePosWindow { get; set; }

        public Model.Vec2Float MousePosWorld { get; set; }

        public string[] PressedKeys { get; set; }

        public Model.Camera Camera { get; set; }

        public int PlayerIndex { get; set; }

        public static DebugState ReadFrom(System.IO.BinaryReader reader)
        {
            Vec2Int windowSize = Model.Vec2Int.ReadFrom(reader);
            Vec2Float mousePosWindow = Model.Vec2Float.ReadFrom(reader);
            Vec2Float mousePosWorld = Model.Vec2Float.ReadFrom(reader);
            string[] pressedKeys = new string[reader.ReadInt32()];
            for (int i = 0; i < pressedKeys.Length; i++)
            {
                pressedKeys[i] = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadInt32()));
            }

            Camera camera = Model.Camera.ReadFrom(reader);
            int playerIndex = reader.ReadInt32();
            var result = new DebugState(windowSize, mousePosWindow, mousePosWorld, pressedKeys, camera, playerIndex);
            return result;
        }

        public void WriteTo(System.IO.BinaryWriter writer)
        {
            this.WindowSize.WriteTo(writer);
            this.MousePosWindow.WriteTo(writer);
            this.MousePosWorld.WriteTo(writer);
            writer.Write(this.PressedKeys.Length);
            foreach (var pressedKeysElement in this.PressedKeys)
            {
                var pressedKeysElementData = System.Text.Encoding.UTF8.GetBytes(pressedKeysElement);
                writer.Write(pressedKeysElementData.Length);
                writer.Write(pressedKeysElementData);
            }

            this.Camera.WriteTo(writer);
            writer.Write(this.PlayerIndex);
        }
    }
}
