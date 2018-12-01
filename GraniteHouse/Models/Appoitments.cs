using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models
{
    public class Appoitments
    {
        public int Id { get; set; }
        public DateTime AppoimentDate { get; set; }

        [NotMapped]
        public DateTime AppoitmentTime { get; set; }

        public string CustormerName { get; set; }
        public string CustormerPhoneNumber { get; set; }
        public string CustormerEmail { get; set; }
        public bool isConfirmed { get; set; }

    }
}
