using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFMakerOFc.Model
{
    public class Team
    {
        public string TournamentName { get; set; }
        public string TeamName { get; set; }
        [Key]
        public long TeamID { get; set; }

    }
}
