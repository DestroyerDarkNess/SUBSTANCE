using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Utilities;
using System;
using System.Collections.Generic;

namespace EasyModern.Core.Font
{
    public class FontManager
    {
        //public ImGuiFontBuilder builder = new ImGuiFontBuilder();
        private Dictionary<string, ImFontPtr> DicFonts = new Dictionary<string, ImFontPtr>();

        public void AddFont(string key, byte[] Font, float Size = 12.0f)
        {
            if (!DicFonts.ContainsKey(key)) DicFonts.Add(key, LoadFontFromBytes(Font, Size));
        }

        public ImFontPtr GetFont(string key) => DicFonts.TryGetValue(key, out ImFontPtr value) ? value : ImFontPtr.Null;

        public unsafe ImFontPtr LoadFontFromBytes(byte[] Font, float Size)
        {
            ImFontPtr Result = ImFontPtr.Null; ;
            try
            {
                ImGuiFontBuilder builder = new ImGuiFontBuilder();
                FontBlob item = new FontBlob(Font);
                builder.AddFontFromMemoryTTF(item.Data, item.Length, Size);
                Result = builder.Build();
            }
            catch { Console.WriteLine("Failed to load Font"); }

            return Result;
        }

    }
}
