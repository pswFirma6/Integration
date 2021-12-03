using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Model
{
    public class PharmacyComment
    {
        [Key]
        public int Id { get; set; }
        public string PharmacyName { get; set; }
        public string Content { get; set; }

        public PharmacyComment() { }
        public PharmacyComment (int id, string pharmacyName, string content)
        {
            Id = id;
            PharmacyName = pharmacyName;
            Content = content;
        }
    }
}
