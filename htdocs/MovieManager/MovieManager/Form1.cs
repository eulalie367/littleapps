using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace MovieManager
{

    public partial class Form1 : Form
    {
        protected List<Movie> Files { get; set; }
        protected string Dir { get; set; }
        protected List<Movie.ReplaceMent> Replaces { get; set; }
        string replacementFile = Path.GetFullPath("Configs/replacements.xml");
        string fileNamesFile = Path.GetFullPath("Configs/aviFiles.xml");
        
        public Form1()
        {
            InitializeComponent();

            this.Dir = "c:\\downloads";
            this.Replaces = new List<Movie.ReplaceMent>();

            this.Replaces = this.Replaces.Open(replacementFile);

            dgPhrases.DataSource = this.Replaces;
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.ShowDialog();
            this.Dir = folderBrowserDialog1.SelectedPath;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Files = new List<Movie>();

            //Files = Files.Open(fileNamesFile);

            if (Files.Count < 1)
            {
                if (Files.GetMovies(this.Dir, this.Replaces))
                {
                    if (Files.Save(fileNamesFile))
                        lblDir.Text = "Saved";
                    else
                        lblDir.Text = "Not Saved";
                }
                else
                    lblDir.Text = "No Files Exist.\nChoose another\ndirectory.";
            }
            lblDir.Text = folderBrowserDialog1.SelectedPath + "\n" + Files.Count.ToString() + " avi files found";
            dataGridView1.DataSource = Files;

        }

        private void dgPhrases_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dgPhrases.DataSource != null)
            {
                List<Movie.ReplaceMent> r = dgPhrases.DataSource as List<Movie.ReplaceMent>;
                if(r.Count > 0)
                    r.Save(replacementFile);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void dgPhrases_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddRow();
            }
        }
        
        private void AddRow()
        {
            dgPhrases.DataSource = null;
            Movie.ReplaceMent r = new Movie.ReplaceMent(" ", " ");
            r.UseRegeEx = false;
            this.Replaces.Add(r);
            dgPhrases.DataSource = this.Replaces;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Files = new List<Movie>();

            Files.GetMovies(this.Dir, new List<Movie.ReplaceMent>());
            
            
            lblDir.Text = folderBrowserDialog1.SelectedPath + "\n" + Files.Count.ToString() + " avi files found";

            dataGridView1.DataSource = Files;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Files = new List<Movie>();

            Files.GetMovies(this.Dir, new List<Movie.ReplaceMent>());

            List<Movie.ReplaceMent> newReplacements = new List<Movie.ReplaceMent>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Movie m = Files.Where(f => f.FullPath == (row.Cells[0] == null ? "" : row.Cells[0].Value.ToString())).FirstOrDefault();
                
                if (m != null)
                {
                    string newName = row.Cells[2] == null ? "" : row.Cells[2].Value.ToString();

                    foreach (string word in Regex.Split(m.FileName, "\\W+", RegexOptions.IgnoreCase))
                    {
                        if (!string.IsNullOrEmpty(word) 
                            && word.Length > 3 
                            && !newName.ToLower().Contains(word.ToLower()) 
                            && !newReplacements.Where(f => !string.IsNullOrEmpty(f.Find)).Select(f => f.Find.ToLower()).Contains(word.ToLower())
                            )
                        {
                            newReplacements.Add(new Movie.ReplaceMent { Find = word, Replace = "", UseRegeEx = false });
                        }
                    }
                }
            }
            newReplacements.Add(new Movie.ReplaceMent { Find = "\\W", Replace = " ", UseRegeEx = true });
            newReplacements.Add(new Movie.ReplaceMent { Find = "  ", Replace = " ", UseRegeEx = false });


            lblDir.Text = folderBrowserDialog1.SelectedPath + "\n" + Files.Count.ToString() + " avi files found";

            List<Movie> fi = new List<Movie>();
            fi.GetMovies(this.Dir, newReplacements);

            this.Replaces = newReplacements;
            dgPhrases.DataSource = newReplacements;
            dataGridView1.DataSource = fi;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Movie> movies = dataGridView1.DataSource as List<Movie>;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Selected)
                {
                    Movie f = r.DataBoundItem as Movie;
                    string newFilePath = f.FullPath.Replace(f.FileName, "");
                    newFilePath += f.SavedName.TrimEnd(null).TrimStart(null).Replace(" ", " ").Replace("  ", " ").Replace(" ", "_") + ".avi";
                    System.IO.File.Move(f.FullPath, newFilePath);

                    
                }
            }
        }
    }
}
