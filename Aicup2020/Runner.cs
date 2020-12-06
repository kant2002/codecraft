// <copyright file="Runner.cs" company="Andrii Kurdiumov">
// Copyright (c) Andrii Kurdiumov. All rights reserved.
// </copyright>

namespace Aicup2020
{
    using System;
    using System.IO;
    using System.Net.Sockets;

    public class Runner
    {
        private BinaryReader reader;
        private BinaryWriter writer;
        private TcpClient client;

        public Runner(string host, int port, string token)
        {
            this.client = new TcpClient(host, port) { NoDelay = true };
            var stream = new BufferedStream(client.GetStream());
            this.reader = new BinaryReader(stream);
            this.writer = new BinaryWriter(stream);
            var tokenData = System.Text.Encoding.UTF8.GetBytes(token);
            this.writer.Write(tokenData.Length);
            this.writer.Write(tokenData);
            this.writer.Flush();
        }

        public static void Main(string[] args)
        {
            string host = args.Length < 1 ? "127.0.0.1" : args[0];
            int port = args.Length < 2 ? 31001 : int.Parse(args[1]);
            string token = args.Length < 3 ? "0000000000000000" : args[2];
            new Runner(host, port, token).Run();
        }

        public void Run()
        {
            var myStrategy = new MyStrategy();
            var debugInterface = new DebugInterface(this.reader, this.writer);
            var running = true;
            while (running)
            {
                switch (Model.ServerMessage.ReadFrom(this.reader))
                {
                    case Model.ServerMessage.GetAction message:
                        new Model.ClientMessage.ActionMessage(myStrategy.GetAction(message.PlayerView, message.DebugAvailable ? debugInterface : null)).WriteTo(this.writer);
                        this.writer.Flush();
                        break;
                    case Model.ServerMessage.Finish message:
                        running = false;
                        break;
                    case Model.ServerMessage.DebugUpdate message:
                        myStrategy.DebugUpdate(message.PlayerView, debugInterface);
                        new Model.ClientMessage.DebugUpdateDone().WriteTo(this.writer);
                        this.writer.Flush();
                        break;
                    default:
                        throw new Exception("Unexpected server message");
                }
            }
        }

        public void Stop()
        {
            client.Close();
        }
    }
}