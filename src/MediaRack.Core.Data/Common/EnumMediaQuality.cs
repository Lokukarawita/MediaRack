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
        [Description("x265")]
        x265,
        [Description("Xvid")]
        Xvid,
        [Description("DivX")]
        DivX,
        [Description("WMV")]
        WMV,
        [Description("VP6/7/8/9")]
        VP,
        [Description("AVC")]
        AVC,
        [Description("HEVC")]
        HEVC,
        [Description("MP4V")]
        MP4V,
        [Description("MPEG-2")]
        MPEG2,

        //--- Audio ---
        [Description("MP3")]
        MP3,
        [Description("AAC")]
        AAC,
        [Description("AC3")]
        AC3,
        [Description("WMA")]
        WMA,
        [Description("OGG")]
        OGG,
        [Description("WAV/PCM")]
        WAV,

        //--- Containers ---
        [Description("MP4")]
        MP4,
        [Description("AVI")]
        AVI,
        [Description("MKV")]
        MKV,
        [Description("WebM")]
        WEBM,
        [Description("FLV")]
        FLV,
        [Description("QT")]
        QT,
        [Description("MOV")]
        MOV,
        [Description("VOB")]
        VOB,

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
        [Description("480p")]
        HD480P,
        [Description("4k")]
        HD4K,
        
        //--- Defaults ---
        [Description("Audio")]
        Audio = 1000,
        [Description("Video")]
        Video = 1001,
    }
}
