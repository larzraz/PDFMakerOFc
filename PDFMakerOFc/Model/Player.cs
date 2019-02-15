using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFMakerOFc.Model
{
    public class Player
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public DateTime Birthdate { get; set; }
        public int TeamID { get; set; }

        public bool IsGoalie { get; set; }
        public bool IsCaptain { get; set; }

    }
}
