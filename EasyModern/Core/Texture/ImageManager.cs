using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;

namespace EasyModern.Core.Texture
{
    public class ImageManager
    {
        private SharpDX.Direct3D9.Device device = null;

        public ImageManager(SharpDX.Direct3D9.Device D3Ddevice) { device = D3Ddevice; }

        private Dictionary<string, ImTextureID> DicImages = new Dictionary<string, ImTextureID>();

        public void AddImage(string key, System.Drawing.Image image)
        {
            if (device != null && !DicImages.ContainsKey(key)) DicImages.Add(key, LoadTextureFromImage(image, device));
        }

        public ImTextureID GetImage(string key) => DicImages.TryGetValue(key, out ImTextureID value) ? value : ImTextureID.Null;

        public ImTextureID LoadTextureFromImage(System.Drawing.Image image, SharpDX.Direct3D9.Device device)
        {
            ImTextureID Result = ImTextureID.Null; ;
            try
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    ms.Position = 0;
                    SharpDX.Direct3D9.Texture texture = SharpDX.Direct3D9.Texture.FromStream(device, ms);
                    Result = new ImTextureID(texture.NativePointer);
                }
            }
            catch { Console.WriteLine("Failed to load image"); }

            return Result;
        }

    }
}
