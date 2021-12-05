using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class PharmacyCommentDTO
    {
        public string PharmacyName { get; set; }
        public string Content { get; set; }

        public PharmacyCommentDTO() { }
        public PharmacyCommentDTO(string pharmacyName, string content)
        {
            PharmacyName = pharmacyName;
            Content = content;
        }
    }
}
