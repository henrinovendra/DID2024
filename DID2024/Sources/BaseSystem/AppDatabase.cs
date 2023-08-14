using System;
using System.Threading.Tasks;

namespace DID
{

    public class AppDatabase
    {

        
         public async static Task DatabasePrepare()
        {
        }
        public async static Task ListIntoMemory()
        {
            await DID.DataLayers.KodeAplikasi.ListIntoMemory();
            System.GC.Collect();
        }

    }


}