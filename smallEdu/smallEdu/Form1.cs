using System;
using System.Threading;
using System.Threading.Tasks;
namespace smallEdu
{
    public partial class smallEdu : Form
    {
        const String notAllowedNameCharecter = "[0-9 \\[\\]{ \"}~`!@#$%\\^&\\*()\\-_+=|\\':;/><]+";
        const String notAllowedContactCharecter = "[a-z A-Z\\[\\]{ \"}~`!@#$%\\^&\\*()\\-_+=|\\':;/><]";
        
        public LocalDatabase lbDataBase;
        public static st_Standards st_StdCount;
        private DebugLogger log;
        private offlineDatabase offlineData = null;
        public smallEdu(string s_dataBasePath)
        {
            InitializeComponent();
           
            log = new DebugLogger();
            if(File.Exists(Mislaneous.s_debugStatementFile))
            {
                File.Delete(Mislaneous.s_debugStatementFile);
            }
            st_StdCount = new st_Standards(this.CB_newStudentStanderd.Items.Count - 1, this.CB_newStudentStanderd.Items );
            log.logDebugStatement("Standard Counts : " + st_StdCount.int_Standards.ToString()+System.Environment.NewLine);
#if false
            lbDataBase = new LocalDatabase();
             lbDataBase.initDataBase();
            offlineData = new offlineDatabase();
            offlineData.createOfflineDataBase();
#endif




        }

        private void BT_logIn_Click(object sender, EventArgs e)
        {
        }

        private void CAL_dateOfBirth_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.TB_dateOfBirth.Text = this.CAL_dateOfBirth.SelectionRange.Start.ToShortDateString();
        }

        private void CAL_newStudentDateOfBirth_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.TB_newStudentDateOfBirth.Text = this.CAL_newStudentDateOfBirth.SelectionRange.Start.ToShortDateString();
        }

        private void BT_newStudentBrowseImage_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fd = new OpenFileDialog();
                fd.Filter = "Image Files(*.jpg; *.jpeg)|*.jpg; *.jpeg";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(fd.FileName);
                    Image img = Image.FromFile(fd.FileName);
                  
                        /*resize the image according to picture box*/
                        img = img.GetThumbnailImage(PB_newStudentPicture.Width, PB_newStudentPicture.Height, null, IntPtr.Zero);
                        PB_newStudentPicture.Image = img;
                        PB_newStudentPicture.BackgroundImageLayout = ImageLayout.Center;          
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
               // File.AppendAllLines();
            }
            
        }

       

        private void TB_newStudentFatherName_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(TB_newStudentFatherName.Text, notAllowedNameCharecter))
            {
                LB_newStudentFatherName.ForeColor = Color.Red;
            }
            else
            {
                LB_newStudentFatherName.ForeColor = Color.Black;
            }

        }

        private void TB_newStudentMotherName_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(TB_newStudentMotherName.Text, notAllowedNameCharecter))
            {
                LB_newStudentMotherName.ForeColor = Color.Red;
            }
            else
            {
                LB_newStudentMotherName.ForeColor = Color.Black;
            }

        }

        private void TB_newStudentFullName_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(TB_newStudentFullName.Text, notAllowedNameCharecter))
            {
                LB_newStudent.ForeColor = Color.Red;
            }
            else
            {
                LB_newStudent.ForeColor = Color.Black;
            }
        }

        private void CB_newStudentStanderd_SelectedIndexChanged(object sender, EventArgs e)
        {


            Directory.CreateDirectory("Database\\" + CB_newStudentStanderd.SelectedIndex.ToString());

            try
            {
                string s_file = ""; 
     
                log.logDebugStatement(CB_newStudentStanderd.SelectedIndex.ToString());
                switch ((Mislaneous.E_streem)CB_newStudentStanderd.SelectedIndex)
                {
                    case Mislaneous.E_streem.XI:
                    case Mislaneous.E_streem.XII:
                    case Mislaneous.E_streem.Bachelor:
                    case Mislaneous.E_streem.Master:
                        CB_newStudentStreem.Enabled = true;     /*Enable streem selection combobox*/
                        CB_newStudentStreem.Items.Clear();
                        s_file = Mislaneous.s_streemPath + Enum.GetName(typeof(Mislaneous.E_streem), CB_newStudentStanderd.SelectedIndex) + Mislaneous.s_streemFileExtn;
                        String[] s_lines = File.ReadAllLines(s_file);
                        CB_newStudentStreem.Items.AddRange(s_lines);
                        break;
                    default:
                        CB_newStudentStreem.Enabled = false;  /*Enable streem selection combobox*/
                        CB_newStudentStreem.Items.Clear();
                        break;
                }
                 //= s_lines[0];
               

       
                /* switch ((Mislaneous.E_streem)CB_newStudentStanderd.SelectedIndex)
                {
                    case Mislaneous.E_streem.XI:
                        CB_newStudentStreem.Items.Add(File.ReadAllText(s_file));
                        break;
                    case Mislaneous.E_streem.XII:
                        break;
                    case Mislaneous.E_streem.Bachelor:
                        break;
                    case Mislaneous.E_streem.Master:
                        break;
                }*/
            }
            catch(Exception ex)
            {
                log.logDebugStatement(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void TB_newStudentContactNumber_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(TB_newStudentContactNumber.Text, notAllowedContactCharecter))
            {
                LB_newStudentContactNumber.ForeColor = Color.Red;
            }
            else
            {
                LB_newStudentContactNumber.ForeColor = Color.Black;
            }
        }

        private async void BT_newStudentSaveInfo_Click(object sender, EventArgs e)
        {

            GB_addNewStudent.Enabled = false;
            LB_newStudentSaveInfoProgressStatus.Enabled = true;
            PB_newStudentSaveInfoProgress.Enabled = true;
            LB_newStudentSaveInfoProgressStatus.Visible = true;
            PB_newStudentSaveInfoProgress.Visible = true;
            Thread.Sleep(20);

            
               await Task.Run(()=>  {
           String now = DateTime.Now.ToString();
           char[] trm = { ':','\\', '/',' ',',','P','M'};
           for (int i = 1; i <= 100; i++)
                {

                    PB_newStudentSaveInfoProgress.Invoke((Action)(() => PB_newStudentSaveInfoProgress.Value = i));
                    //PB_newStudentSaveInfoProgress.Value = i;
                    
               LB_newStudentSaveInfoProgressStatus.Invoke((Action)(() => LB_newStudentSaveInfoProgressStatus.Text = "Saving..."+i.ToString()));
               Thread.Sleep(40);

           }
        
           LB_newStudentSaveInfoProgressStatus.Invoke((Action)(() => LB_newStudentSaveInfoProgressStatus.Text = now.Trim(new Char[] { ' ', '*', '.' ,'/',' ','P','M',':'})));
           Thread.Sleep(5000);
       });
            GB_addNewStudent.Enabled = true;
      
            LB_newStudentSaveInfoProgressStatus.Enabled = false;
            PB_newStudentSaveInfoProgress.Enabled = false;
            LB_newStudentSaveInfoProgressStatus.Visible = false;
            PB_newStudentSaveInfoProgress.Visible = false;


        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }

    public class DebugLogger
    {
        public void logDebugStatement(string statement)
        {
            Console.WriteLine("Debug statement" + ": " + statement);
            File.AppendAllText(Mislaneous.s_debugStatementFile,statement);
        }
    }
}