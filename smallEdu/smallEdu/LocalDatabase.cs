using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

/*
 * Database operation
 
 */
namespace smallEdu
{
    public class LocalDatabase
    {
        private SqlConnection sqlConnection = null;
        private SqlCommand sqlCommand = null;
        private String sqlQuery = null;
        private string ConnectionString = "Integrated Security=SSPI;" +"Initial Catalog=;" + "Data Source=localhost;";
        private String dataBaseName = "smallEduDatabase";
        private DebugLogger log;/*to log debug statements*/
        public LocalDatabase()
        {
            log = new DebugLogger();
            sqlConnection = new SqlConnection();
        }




        public bool connectToDataBase()
        {
            bool connSuccess = false;
            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }
                sqlQuery = "CREATE DATABASE " + dataBaseName + "ON PRIMARY"
                    + "(Name=" + dataBaseName + ", filename = '" + Mislaneous.dataBaseDirectory + "\\" + dataBaseName + ".mdf', size=3,"
                    + "maxsize=5, filegrowth=20%)log on"
                    + "(name=logdatabase,filename='" + Mislaneous.dataBaseDirectory + "\\" + dataBaseName + ".ldf', size=3,maxsize=20,filegrowth=1)";
                executeSqlQuery(sqlQuery);
            }
            catch (Exception ex)
            {

            }
            

            return connSuccess;
        }


        public void executeSqlQuery(string sqlQuery)
        {
            if(sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            ConnectionString = "Integrated Sequrity=SSPI" +
                "Initial Catalog=" + dataBaseName + ";Data Source=localhost";
            sqlConnection.ConnectionString = ConnectionString;
            sqlConnection.Open();
            sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
        }
       public bool initDataBase()
        {
            bool initSuccess = false;
            try
            {
                if (!Directory.Exists(Mislaneous.dataBaseDirectory))
                {
                    Directory.CreateDirectory(Mislaneous.dataBaseDirectory);
                }
                if (smallEdu.st_StdCount.var_item != null)
                {
                    foreach(object str in smallEdu.st_StdCount.var_item)
                    {
                        log.logDebugStatement("Standard : " + str.ToString() + System.Environment.NewLine);                      
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR!");
            }
            return initSuccess;
        }
    }
}
