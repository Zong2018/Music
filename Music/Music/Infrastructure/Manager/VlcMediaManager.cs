using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Vlc.DotNet.Core;
using Vlc.DotNet.Wpf;

namespace Music.Infrastructure.Manager
{
    public class VlcMediaManager
    {
        static readonly object _lockObject = new object();

        static VlcControl vlcControl = null;
        public static VlcControl VlcControl
        {
            get
            {
                if (vlcControl == null)  //先判断实例是否存在，不存在再加锁处理
                {
                    lock (_lockObject)
                    {

                        vlcControl = new VlcControl();
                        var vlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(System.Environment.CurrentDirectory, @"libvlc\win-x86"));
                        //初始化播放器
                        vlcControl.SourceProvider.CreatePlayer(vlcLibDirectory);
                    }
                }
                return vlcControl;
            }
        }

        public static VlcMediaPlayer MediaPlayer
        {
            get
            {
                return VlcControl.SourceProvider.MediaPlayer;
            }
        }

        /// <summary>
        /// 设置播放位置(拖动)
        /// </summary>
        /// <param name="libvlc_mediaplayer"></param>
        /// <param name="time"></param>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void libvlc_media_player_set_time(IntPtr libvlc_mediaplayer, Int64 time);


        //public static void Play(Uri uri, params string[] options)
        //{
        //    MediaPlayer.Play(uri, options);
        //}

        //public void Play(Uri uri, params string[] options);
        //public void Play();
        //public void Play(FileInfo file, params string[] options);
        //public void Play(Stream stream, params string[] options);
        //public void Play(string mrl, params string[] options);

    }
}
