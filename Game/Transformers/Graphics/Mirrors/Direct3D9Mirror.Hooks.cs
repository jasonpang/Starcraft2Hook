using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Game.Extensions.SharpDX;
using Game.Hooks.Graphics;
using Logging.Extensions;
using SharpDX.Direct3D9;

namespace Game.Transformers.Graphics.Mirrors
{
    public partial class Direct3D9Mirror
    {
        private object lock1 = new object();
        private Dictionary<String, Texture> TextureDictionary = new Dictionary<string, Texture>();
        private int numSaves = 50;

        private void Hook_OnSetTexture (ref IntPtr devicePointer, ref int stage, ref IntPtr texturePointer)
        {
            Log.LogMethodSignatureTypesAndValues(devicePointer, stage, texturePointer);
            IntPtr _devicePointer = devicePointer;
            int _stage = stage;
            IntPtr _texturePointer = texturePointer;

            try
            {
                {
                    lock (lock1)
                    {
                        Task.Factory.StartNew(() =>
                                                   {
                                                       try
                                                       {
                                                           var baseTexture = new BaseTexture(_texturePointer);

                                                           if (baseTexture.TypeInfo == ResourceType.Texture &&
                                                               numSaves > 0)
                                                           {

                                                               var dataStream = Texture.ToStream(baseTexture,
                                                                                                  ImageFileFormat.Png);

                                                               var hash = dataStream.GetMD5HashString();
                                                               this.Log.Info("Got hash: {0}.", hash);

                                                               if (!this.TextureDictionary.ContainsKey(hash))
                                                               {
                                                                   TextureDictionary.Add(hash,
                                                                                          new Texture(_texturePointer));
                                                                   this.Log.Info("Saving file with hash {0}.", hash);
                                                                   Texture.ToFile(baseTexture,
                                                                                   String.Format(
                                                                                                  @"C:\Sc2Ai\Textures\{0}.png",
                                                                                                  hash.Substring(0,
                                                                                                                  hash
                                                                                                                      .Length -
                                                                                                                  2)),
                                                                                   ImageFileFormat.Png);
                                                                   numSaves--;
                                                               }

                                                           }
                                                       }
                                                       catch (Exception ex)
                                                       {
                                                           Log.Warn(ex);
                                                       }
                                                   }, TaskCreationOptions.LongRunning);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Warn(ex);
            }
        }

        void Hook_OnCreateTexture(ref IntPtr devicePointer, ref int width, ref int height, ref int levels, ref Usage usage, ref Format format, ref Pool pool, ref IntPtr texture, ref IntPtr sharedHandle)
        {
            Log.LogMethodSignatureTypesAndValues(devicePointer, width, height, levels, usage, format, pool, "out", sharedHandle);
            IntPtr _devicePointer = devicePointer;
            int _width = width;
            int _height = height;
            int _levels = levels;
            Usage _usage = usage;
            Format _format = format;
            Pool _pool = pool;
            IntPtr _sharedHandle = sharedHandle;
            IntPtr _texturePointer = IntPtr.Zero;

            try
            {
                {
                    lock (lock1)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                var baseTexture = new BaseTexture(_texturePointer);

                                if (baseTexture.TypeInfo == ResourceType.Texture &&
                                    numSaves > 0)
                                {

                                    var dataStream = Texture.ToStream(baseTexture,
                                                                       ImageFileFormat.Png);

                                    var hash = dataStream.GetMD5HashString();
                                    this.Log.Info("Got hash: {0}.", hash);

                                    if (!this.TextureDictionary.ContainsKey(hash))
                                    {
                                        TextureDictionary.Add(hash,
                                                               new Texture(_texturePointer));
                                        this.Log.Info("Saving file with hash {0}.", hash);
                                        Texture.ToFile(baseTexture,
                                                        String.Format(
                                                                       @"C:\Sc2Ai\Textures\{0}.png",
                                                                       hash.Substring(0,
                                                                                       hash
                                                                                           .Length -
                                                                                       2)),
                                                        ImageFileFormat.Png);
                                        numSaves--;
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Warn(ex);
                            }
                        }, TaskCreationOptions.LongRunning);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Warn(ex);
            }
        }
    }
}
