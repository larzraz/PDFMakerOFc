using PDFMakerOFc.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFMakerOFc.ViewModels
{
    public class MainViewModel
    {
        DataContext db = new DataContext();
        public List<Player> players;
        public List<Team> teams;
        public ObservableCollection<Player> Player { get; set; } 
        public ObservableCollection<Team> Team;

        public MainViewModel()
        {
            players = db.Players.ToList();
            teams = db.Teams.ToList();
            Player = new ObservableCollection<Player>(players);
            Team = new ObservableCollection<Team>(teams);
        }

    }
}
