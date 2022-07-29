#region license

// Copyright (c) 2021, andreakarasho
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 1. Redistributions of source code must retain the above copyright
//    notice, this list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright
//    notice, this list of conditions and the following disclaimer in the
//    documentation and/or other materials provided with the distribution.
// 3. All advertising materials mentioning features or use of this software
//    must display the following acknowledgement:
//    This product includes software developed by andreakarasho - https://github.com/andreakarasho
// 4. Neither the name of the copyright holder nor the
//    names of its contributors may be used to endorse or promote products
//    derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS ''AS IS'' AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ClassicUO.Configuration;
using ClassicUO.Game;
using ClassicUO.Game.Data;
using ClassicUO.Game.Managers;
using ClassicUO.IO;
using ClassicUO.IO.Resources;
using ClassicUO.Renderer;
using ClassicUO.Renderer.Batching;
using ClassicUO.Utility.Logging;
using ClassicUO.Utility.Platforms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SDL2;

namespace ClassicUO.Network
{
    internal unsafe class Plugin
    {
        private Plugin(string path)
        {
            PluginPath = path;
        }

        public static List<Plugin> Plugins { get; } = new List<Plugin>();

        public string PluginPath { get; }

        public bool IsValid { get; private set; }


        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteFile(string name);


        public static Plugin Create(string path)
        {
            path = Path.GetFullPath(Path.Combine(CUOEnviroment.ExecutablePath, "Data", "Plugins", path));

            if (!File.Exists(path))
            {
                Log.Error($"Plugin '{path}' not found.");

                return null;
            }

            Log.Trace($"Loading plugin: {path}");

            Plugin p = new Plugin(path);
            p.Load();

            if (!p.IsValid)
            {
                Log.Warn($"Invalid plugin: {path}");

                return null;
            }

            Log.Trace($"Plugin: {path} loaded.");
            Plugins.Add(p);

            return p;
        }


        public void Load()
        {
        }


        internal static void Tick()
        {
        }


        internal static bool ProcessRecvPacket(byte[] data, ref int length)
        {
            bool result = true;

            return result;
        }

        internal static bool ProcessSendPacket(byte[] data, ref int length)
        {
            bool result = true;

            return result;
        }

        internal static void OnClosing()
        {
        }

        internal static void OnFocusGained()
        {
        }

        internal static void OnFocusLost()
        {
        }


        internal static void OnConnected()
        {
        }

        internal static void OnDisconnected()
        {
        }

        internal static bool ProcessHotkeys(int key, int mod, bool ispressed)
        {
            bool result = true;

            return result;
        }

        internal static void ProcessMouse(int button, int wheel)
        {
        }

        internal static void ProcessDrawCmdList(GraphicsDevice device)
        {
        }

        internal static int ProcessWndProc(SDL.SDL_Event* e)
        {
            int result = 0;

            return result;
        }

        internal static void UpdatePlayerPosition(int x, int y, int z)
        {
        }


        //Code from https://stackoverflow.com/questions/6374673/unblock-file-from-within-net-4-c-sharp
        private static void UnblockPath(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);

            foreach (string file in files)
            {
                if (file.EndsWith("dll") || file.EndsWith("exe"))
                {
                    UnblockFile(file);
                }
            }

            foreach (string dir in dirs)
            {
                UnblockPath(dir);
            }
        }

        private static bool UnblockFile(string fileName)
        {
            return DeleteFile(fileName + ":Zone.Identifier");
        }
    }
}