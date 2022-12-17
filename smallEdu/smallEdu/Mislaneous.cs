using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smallEdu
{
    public class Mislaneous
    {
        public static string s_newStudentImageFilePath = null;
        public static string s_debugStatementFile = @"debug.txt";
        public static string dataBaseDirectory = @"Database";
        public static string s_streemPath = "courselist\\";
        public static string s_streemFileExtn = ".txt";
        public enum E_streem { XI = 14, XII, Bachelor, Master };

    }

    public struct st_Standards
    {
        public int int_Standards = -1;
        public ComboBox.ObjectCollection var_item = null;
        public st_Standards(int std, ComboBox.ObjectCollection obj)
        {
            int_Standards = std;
            var_item = obj;
        }
    }

   
}
