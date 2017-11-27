using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using MediaRack.Core.Data.Common.Metadata;
using MediaRack.Core.Data.Common;
using MediaRack.Core.Data.Local.DAO;
using MediaRack.Core.Data.Remote;

namespace Testings
{
    public static class EnumExtentions
    {
        public static string GetDescription(this Enum e)
        {
            var type = e.GetType();
            var members = type.GetMember(e.ToString());
            if (members != null && members.Length > 0)
            {
                var attribs = members[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attribs != null && attribs.Length > 0)
                {
                    return ((DescriptionAttribute)attribs[0]).Description;
                }
            }

            return e.ToString();
        }
    }

    class Program
    {

      
        
        
        
        
        enum QLT
        {
            [Description("A Enum")]
            A,
            B,
            C,
            D
        }

        static void Main(string[] args)
        {
            //MediaRack.Core.Scanning.DirectoryWatch w = new MediaRack.Core.Scanning.DirectoryWatch(@"D:\I files\My\MV\Damith");
            //Console.ReadLine();

            //var took = MediaRack.Core.Data.Local.LocalFileQueue.Instance.TakeFile();
            //Console.ReadLine();

           //var item =  MediaRack.Core.Scanning.LocalFileQueue.Instance.Dequeue();




           // System.IO.FileSystemWatcher fsw = new System.IO.FileSystemWatcher(@"D:\XOX");
           // fsw.Created += (x, y) => {
           //     Console.WriteLine("Created " + y.FullPath);
           // };
           // fsw.Filter = "*.txt";
           // fsw.NotifyFilter = System.IO.NotifyFilters.LastWrite;
           // fsw.EnableRaisingEvents = true;

           // Console.ReadLine();


            //MediaRack.Core.Data.Remote.MYSQLRemoteStorage s = new MediaRack.Core.Data.Remote.MYSQLRemoteStorage();
            //s.Connect();
            ////s.SignUp("heshan", "elooo", new UserSettingsMetaInfo());
            ////s.CheckAvailability("heshan");
            //var u= s.Authorize("heshan", "elooo");
            //s.ChangePassword("elooo", "elo");
            ////set.WatchDir.Add(@"C:\D");
            ////u.Settings.ConflictProtocol = ConflictResolution.KeepRemote;
            ////s.UpdateUserSettings(u.Settings);
            
            var dao = new MediaEntryDAO();

            var finfo = new FileMetaInfo()
            {
                AbsolutePath = @"D:\hdd\Movies\Animation\Big Hero 6 (2014) [1080p]\Big.Hero.6.2014.1080p.BluRay.x264.YIFY.mp4",
                FileName = "Big.Hero.6.2014.1080p.BluRay.x264.YIFY.mp4",
                PcName = "Hornet-NBI3",
                RootRelativePath = @"hdd\Movies\Animation\Big Hero 6 (2014) [1080p]\Big.Hero.6.2014.1080p.BluRay.x264.YIFY.mp4",
                Root = @"D:\",
                Dimensions = new System.Drawing.Size(1920, 1080),
                Group = "A"
            };
            finfo.QualityTags.Add(MediaQuality.BluRayRip);
            finfo.QualityTags.Add(MediaQuality.x264);
            finfo.QualityTags.Add(MediaQuality.ACC);
            finfo.QualityTags.Add(MediaQuality.MKV);
            finfo.QualityTags.Add(MediaQuality.HD1080P);
            finfo.QualityTags.Add(MediaQuality.Audio);
            finfo.QualityTags.Add(MediaQuality.Video);
            finfo.Subtitles.Add(new SubtitleMetaInfo() { IsEmbedded = true, Language = "English" });

            var compos = new CompositionMetaInfo()
            {
                Title = "Big Hero 6",
                Rating = 8.1,
                Released = new DateTime(2014, 1, 1),
                Overview = "Big airbag hero"
            };

            var entry = new MediaEntry();
            entry.Classification = MediaClassification.Movie;
            entry.Watched = false;
            entry.LocalStatus = LocalSyncStatus.NEW;
            entry.FileInfo.Files.Add(finfo);
            entry.CompositionInfo = compos;
            entry.IDInfo = new IDMetaInfo() { TmdbID = 2123, ImdbID = "tt22343454" };

            dao.Add(entry);

            //var session = MediaRack.Core.Data.ORM.ORMFactory.Instance.GetSession();
            //session.SaveOrUpdate(new MediaRack.Core.Data.Common.MediaEntry()
            //{
            //    LocalStatus = LocalSyncStatus.NEW,
            //    FileInfo = new FileCollectionMetaInfo(),
            //    Watched = true,
            //    WatchedOn = DateTime.Now,
            //    Grade = "A",
            //    Comment = "First instert",
            //    Timestamp = DateTime.Now
            //});


            //var str = QLT.B.GetDescription();

            //HashSet<QLT> s = new HashSet<QLT>();
            //s.Add(QLT.A);
            //s.Add(QLT.B);
            //s.Add(QLT.A);
            //s.Add(QLT.D);

            //var str = JsonConvert.SerializeObject(s);
            //var de = JsonConvert.DeserializeObject<HashSet<QLT>>(str);
        }
    }
}
