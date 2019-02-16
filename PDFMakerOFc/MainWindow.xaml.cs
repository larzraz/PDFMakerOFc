using PDFMakerOFc.Model;
using PDFMakerOFc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BitMiracle;
using BitMiracle.Docotic.Pdf;

namespace PDFMakerOFc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
        public int MatchNo { get; set; }
        public DateTime MatchDate { get; set; }
        public string TeamNameString { get; set; }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            PDFMakerOFc.PDFOFCDataSet pDFOFCDataSet = ((PDFMakerOFc.PDFOFCDataSet)(this.FindResource("pDFOFCDataSet")));
            // Load data into the table Teams. You can modify this code as needed.
            PDFMakerOFc.PDFOFCDataSetTableAdapters.TeamsTableAdapter pDFOFCDataSetTeamsTableAdapter = new PDFMakerOFc.PDFOFCDataSetTableAdapters.TeamsTableAdapter();
            pDFOFCDataSetTeamsTableAdapter.Fill(pDFOFCDataSet.Teams);
            System.Windows.Data.CollectionViewSource teamsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("teamsViewSource")));
            teamsViewSource.View.MoveCurrentToFirst();
            // Load data into the table Players. You can modify this code as needed.
            PDFMakerOFc.PDFOFCDataSetTableAdapters.PlayersTableAdapter pDFOFCDataSetPlayersTableAdapter = new PDFMakerOFc.PDFOFCDataSetTableAdapters.PlayersTableAdapter();
            pDFOFCDataSetPlayersTableAdapter.Fill(pDFOFCDataSet.Players);
            System.Windows.Data.CollectionViewSource playersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("playersViewSource")));
            playersViewSource.View.MoveCurrentToFirst();
        }

        private void GenerateList_Button_Click(object sender, RoutedEventArgs e)
        {
            List<Player> selectedPlayers = playersDataGrid.SelectedItems.Cast<Player>().OrderBy(x => x.Number).ToList();
            MatchNo = Int32.Parse(MatchNo_TextBox.Text);
            MatchDate = Match_calendar.DisplayDate;
            TeamNameString = teamsComboBox.Text;
            string pathToFile = "C:/Users/Larz/Desktop/OFCEFBSS.pdf";

            List<Player> selectedPlayersWithoutGoalieAndC = playersDataGrid.SelectedItems.Cast<Player>().Where(x => x.IsCaptain == false).Where(x => x.IsGoalie == false).OrderBy(x => x.Number).ToList();

            using (PdfDocument pdf = new PdfDocument("C:/Users/Larz/Desktop/OFCEFBS.pdf"))
            {
                foreach (PdfControl control in pdf.GetControls())
                {
                    switch (control.Name)
                    {
                        case "H-hold":
                            if(TeamNameString == "Herre 2")
                            {
                                ((PdfTextBox)control).Text = "Odense Floorball Club 2";
                                break;
                            }
                            else {
                                ((PdfTextBox)control).Text = "Odense Floorball Club";
                                break;
                            }

                        case "Række":
                            if (TeamNameString == "Herre 2")
                            {
                                ((PdfTextBox)control).Text = "2. division Syd";
                                break;
                            }
                            else
                            {
                                ((PdfTextBox)control).Text = "1. division Vest";
                                break;
                            }


                        case "Spillested":
                            ((PdfTextBox)control).Text = "Marienlystcentret";
                            break;
                        case "Kampnr":
                            ((PdfTextBox)control).Text = " " + MatchNo.ToString();
                            break;
                        case "Dato":
                            ((PdfTextBox)control).Text = " " + MatchDate.ToString("dd-MM-yy");
                            break;
                        case "HN1":
                            foreach (var player in selectedPlayers)
                            {
                                if (player.IsCaptain == true)
                                {
                                    ((PdfTextBox)control).Text = player.Number.ToString();
                                    break;
                                }
                            }
                            break;

                        case "HS1":
                            foreach (var player in selectedPlayers)
                            {
                                if (player.IsCaptain == true)
                                {
                                    ((PdfTextBox)control).Text = " " + player.Name;
                                    break;
                                }
                            }
                            break;

                        case "HF1":
                            foreach (var player in selectedPlayers)
                            {
                                if (player.IsCaptain == true)
                                {
                                    ((PdfTextBox)control).Text = " " + player.Birthdate.ToString("dd-MM-yy"); 
                                    break;
                                }
                            }
                            break;

                        case "HN2":
                            foreach (var player in selectedPlayers)
                            {
                                if (player.IsGoalie == true)
                                {
                                    ((PdfTextBox)control).Text = player.Number.ToString();
                                    break;
                                }
                            }
                            break;

                        case "HS2":
                            foreach (var player in selectedPlayers)
                            {
                                if (player.IsGoalie == true)
                                {
                                    ((PdfTextBox)control).Text = " " + player.Name;
                                    break;
                                }
                            }
                            break;

                        case "HF2":
                            foreach (var player in selectedPlayers)
                            {
                                if (player.IsGoalie == true)
                                {
                                    ((PdfTextBox)control).Text = " " + player.Birthdate.ToString("dd-MM-yy");
                                    break;
                                }
                            }
                            break;

                    }
                }
                for(int i =0 ; i < selectedPlayersWithoutGoalieAndC.Count(); i++)
                {
                    string numberString = "HN" + (i + 3);
                    PdfControl number = pdf.GetControl(numberString);
                    ((PdfTextBox)number).Text = selectedPlayersWithoutGoalieAndC[i].Number.ToString();
                    PdfControl name = pdf.GetControl("HS" + (i+3));
                    ((PdfTextBox)name).Text = " " + selectedPlayersWithoutGoalieAndC[i].Name;
                    PdfControl birthdate = pdf.GetControl("HF" + (i+3));
                    ((PdfTextBox)birthdate).Text = " " + selectedPlayersWithoutGoalieAndC[i].Birthdate.ToString("dd-MM-yy");
                }
                pdf.Save(pathToFile);
            }
        }
    }
}
