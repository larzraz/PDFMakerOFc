using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFMakerOFc.Model
{
    public class DataContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DataContext() : base(@"Server=(localdb)\mssqllocaldb;Database=PDFOFC;Trusted_Connection=True;")
        {
           
            //if(Players.Count() == 0)
            //{
            //    Players.Add(new Player { Name = "Jan Jakobsen", Birthdate = DateTime.Now.Date, Number = 75, TeamID = 4 });
            //}
            //if(Teams.Count() == 0)
            //{
            //    Teams.Add(new Team { TeamName = "Odense Floorball Club", TournamentName = "1.Division Vest" });
            //}
        }
    }
}
