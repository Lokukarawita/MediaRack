using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Common
{
    [Flags]
    public enum MediaQuality
    {
        X264 = 2,
        X265 = 4,
        Xvid = 8,
        Web = 16,
        HD720P = 32,
        HD1080P = 64,
        MP3 = 128,
        ACC = 256,
        AC3 = 512,
        Unknown = 2046,
    }
}
