namespace smallEdu
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            string s_databasePath = null;
            try
            {
                s_databasePath = System.IO.File.ReadAllText("db_defaultpath.bin");
            }
            catch (FileNotFoundException fnf)
            {
                System.IO.File.WriteAllText("db_defaultpath.bin", "smallEdu_database");
                s_databasePath = "smallEdu_database";

            }

            if(System.IO.Directory.Exists(s_databasePath) == false)
            {
                DialogResult b_configDecision = System.Windows.Forms.MessageBox.Show("Press 'Yes' to set Databse directory.\nPress 'No' to use deault database directory.", "Configuration", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (b_configDecision == DialogResult.Yes)
                {
                    s_databasePath = null;
                    try
                    {
                        System.Windows.Forms.FolderBrowserDialog fl_diagnog = new System.Windows.Forms.FolderBrowserDialog();
                        DialogResult dr_result = fl_diagnog.ShowDialog();
                       
                       
                        Console.WriteLine("selected Path" + s_databasePath);
                        if(DialogResult.OK == dr_result)
                        {
                            s_databasePath = fl_diagnog.SelectedPath + "\\" + "smallEdu_database";
                            System.IO.Directory.CreateDirectory(s_databasePath );
                        
                            System.IO.File.WriteAllText("db_defaultpath.bin", s_databasePath);
                        }
                        else
                        {
                            System.IO.File.ReadAllText(s_databasePath);
                      
                        }
                 

                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Please re-start appplication and select valid database path.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    System.IO.Directory.CreateDirectory(s_databasePath);

                }

            }
           
            //public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton);
          Application.Run(new smallEdu(s_databasePath));
            
        }
    }
}