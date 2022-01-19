using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.DTO
{
    public class TenderParticipantDto
    {
        public string PharmacyName { get; set; }
        public int Participations { get; set; }

        public TenderParticipantDto() { }

        public TenderParticipantDto(string pharmacyName, int participations)
        {
            PharmacyName = pharmacyName;
            Participations = participations;
        }
    }
}
