using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smallEdu
{
    internal class offlineDatabase
    {
		DebugLogger log = null;
		public offlineDatabase()
		{
			log = new DebugLogger();
		}

		public void createOfflineDataBase()
		{

			if(Directory.Exists(Mislaneous.dataBaseDirectory) == false)
            {
				log.logDebugStatement("Directory : " + Mislaneous.dataBaseDirectory + "has beed created.");
				Directory.CreateDirectory(Mislaneous.dataBaseDirectory);

            }		
		}


	}


  
}
