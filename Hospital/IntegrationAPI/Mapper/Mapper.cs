﻿using AutoMapper;
using IntegrationLibrary.Pharmacy.DTO;
using IntegrationLibrary.ReportingAndStatistics.Model;
using IntegrationLibrary.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Mapper
{
    public class Mapper
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<PharmacyPrescription, PrescriptionDTO>();
                CreateMap<PrescriptionDTO, PharmacyPrescription>();
            }

            public PharmacyPrescription MapPharmacyPrescription(PrescriptionDTO dto)
            {
                PharmacyPrescription prescription = new PharmacyPrescription
                {
                    PharmacyName = dto.PharmacyName,
                    Prescription = new Prescription
                    {
                        Id = dto.Id,
                        PrescriptionDate = dto.PrescriptionDate,
                        Therapy = new TherapyInfo
                        {
                            Diagnosis = dto.Diagnosis,
                            TherapyDuration = new DateRange
                            {
                                StartDate = DateTime.Parse(dto.TherapyStart),
                                EndDate  = DateTime.Parse(dto.TherapyEnd)
                            }
                        },
                        InvolvedParties = new PrescriptionInvolvedParties
                        {
                            DoctorName = dto.DoctorName,
                            PatientName = dto.PatientName
                        },
                        Medicine = new MedicineInfo
                        {
                            Name = dto.MedicineName,
                            Quantity = dto.Quantity,
                            RecommendedDose = dto.RecommendedDose
                        }
                    }
                };
                return prescription;
            }
        }
    }
}