using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Common
{
    /// <summary>
    /// Common file qulity and format types
    /// </summary>
    public enum MediaQuality
    {
        //--- Video ---
        [Description("x264")]
        x264,
        [Description("HEVC")]
        x265,
        [Description("Xvid/Divx")]
        XvidDivX,
        [Description("WMV")]
        WMV,
        [Description("VP8/9")]
        VP,

        //--- Audio ---
        [Description("MP3")]
        MP3,
        [Description("ACC")]
        ACC,
        [Description("AC3")]
        AC3,
        [Description("WMA")]
        WMA,
        [Description("OGG")]
        OGG,

        //--- Containers ---
        [Description("MP4")]
        MP4,
        [Description("AVI")]
        AVI,
        [Description("Mkv")]
        MKV,
        [Description("WebM")]
        WEBM,

        //--- Rips ---
        [Description("WebRip")]
        WebRip,
        [Description("DVDRip")]
        DVDRip,
        [Description("Blu-rayRip")]
        BluRayRip,

        //--- Framesize ---
        [Description("720p")]
        HD720P,
        [Description("1080p")]
        HD1080P,
        [Description("4k")]
        HD4K,
        [Description("HDTV")]
        HDTV,
        
        //--- Defaults ---
        [Description("Audio")]
        Audio = 1000,
        [Description("Video")]
        Video = 1001,
    }
}
