using MediaRack.Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Local.DAO
{
    public interface ISyncedControlDTO<T> where T: ISynchronizable
    { 
        void MarkForDelete(T entity);
    }
}
