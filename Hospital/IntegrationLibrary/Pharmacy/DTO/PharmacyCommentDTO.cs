using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class PharmacyCommentDto
    {
        public string PharmacyName { get; set; }
        public string Content { get; set; }

        public PharmacyCommentDto() { }
        public PharmacyCommentDto(string pharmacyName, string content)
        {
            PharmacyName = pharmacyName;
            Content = content;
        }
    }
}
