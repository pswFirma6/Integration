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
        public DateTime CommentDate { get; set; }

        public PharmacyComment() { }
        public PharmacyComment (string pharmacyName, string content)
        {
            PharmacyName = pharmacyName;
            Content = content;
            CommentDate = DateTime.Now;
        }
    }
}
