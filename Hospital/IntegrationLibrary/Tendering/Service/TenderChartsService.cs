using IntegrationLibrary.Pharmacy.DTO;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Shared.Model;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using IntegrationLibrary.Tendering.Repository;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace IntegrationLibrary.Tendering.Service
{
    public class TenderChartsService
    {
        private readonly ITenderOfferRepository offerRepository;
        private readonly TenderOfferService offerService;
        private readonly TenderOfferItemService offerItemService;
        private readonly ITenderRepository tenderRepository;

        private List<TenderParticipantDto> tenderParticipants;
        private List<int> tenders;
        private List<TenderEarningDto> earnings;
        private List<MedicineDto> medicineConsumption;
        private DateTime startDate;
        private DateTime endDate;

        public TenderChartsService(ITenderOfferRepository repository)
        {
            offerRepository = repository;
            offerService = new TenderOfferService(offerRepository);
            DatabaseContext context = new DatabaseContext();
            ITenderOfferItemRepository itemRepository = new TenderOfferItemRepository(context);
            offerItemService = new TenderOfferItemService(itemRepository);
            tenderRepository = new TenderRepository(context);
        }

        public List<TenderParticipantDto> GetTendersParticipants(DateRange dateRange)
        {
            this.startDate = dateRange.StartDate;
            this.endDate = dateRange.EndDate;
            tenderParticipants = new List<TenderParticipantDto>();
            SetTenderParticipants();
            foreach (TenderParticipantDto participant in tenderParticipants)
            {
                SetPharmacyParticipationNumber(participant.PharmacyName);
            }
            return tenderParticipants;
        }

        private void SetTenderParticipants()
        {
            foreach (TenderOffer offer in offerService.GetOffers())
            {
                Tender tender = tenderRepository.FindById(offer.TenderId);
                if (startDate < tender.StartDate && !tender.Opened)
                {
                    if (!IsPharmacyInParticipants(offer.PharmacyName))
                    {
                        TenderParticipantDto participant = new TenderParticipantDto { PharmacyName = offer.PharmacyName, Participations = 0 };
                        tenderParticipants.Add(participant);
                    }
                }
            }
        }

        private bool IsPharmacyInParticipants(string pharmacy)
        {
            foreach (TenderParticipantDto participant in tenderParticipants)
            {
                if (participant.PharmacyName.Equals(pharmacy))
                {
                    return true;
                }
            }
            return false;
        }

        private void SetPharmacyParticipationNumber(string pharmacy)
        {
            tenders = new List<int>();
            foreach (TenderOffer o in offerService.GetOffers())
            {
                Tender tender = tenderRepository.FindById(o.TenderId);
                if (startDate < tender.StartDate && endDate > tender.EndDate)
                {
                    if (!IsTenderChecked(o.TenderId) && o.PharmacyName.Equals(pharmacy))
                    {
                        tenders.Add(o.TenderId);
                        UpdateParticipant(pharmacy);
                    }
                }

            }
        }

        private bool IsTenderChecked(int tenderId)
        {
            foreach (int tender in tenders)
            {
                if (tender == tenderId)
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateParticipant(string participant)
        {
            foreach (TenderParticipantDto p in tenderParticipants)
            {
                if (p.PharmacyName.Equals(participant))
                {
                    p.Participations++;
                }
            }
        }

        public List<TenderParticipantDto> GetTenderWinners(DateRange dateRange)
        {
            this.startDate = dateRange.StartDate;
            this.endDate = dateRange.EndDate;
            tenderParticipants = new List<TenderParticipantDto>();
            SetTenderWinners();
            return tenderParticipants;
        }

        private void SetTenderWinners()
        {
            foreach (TenderOffer offer in offerService.GetWinningOffers())
            {
                Tender tender = tenderRepository.FindById(offer.TenderId);
                if (startDate < tender.StartDate && !tender.Opened)
                {
                    if (!IsPharmacyInParticipants(offer.PharmacyName))
                    {
                        TenderParticipantDto participant = new TenderParticipantDto { PharmacyName = offer.PharmacyName, Participations = 0 };
                        tenderParticipants.Add(participant);
                    }
                    UpdateParticipant(offer.PharmacyName);
                }
            }
        }

        public List<TenderEarningDto> GetWinningOffersPrices(DateRange dateRange)
        {
            this.startDate = dateRange.StartDate;
            this.endDate = dateRange.EndDate;
            List<TenderEarningDto> winnings = new List<TenderEarningDto>();
            foreach (TenderOffer offer in offerService.GetWinningOffers())
            {
                Tender tender = tenderRepository.FindById(offer.TenderId);
                if (startDate < tender.StartDate && !tender.Opened)
                {
                    TenderEarningDto win = new TenderEarningDto { Name = offer.TenderId.ToString(), Earning = offerItemService.GetOfferPrice(offer.Id) };
                    winnings.Add(win);
                }
            }
            return winnings;
        }

        public List<TenderEarningDto> GetPharmaciesEarnings(DateRange dateRange)
        {
            this.startDate = dateRange.StartDate;
            this.endDate = dateRange.EndDate;
            earnings = new List<TenderEarningDto>();
            SetTenderEarnings();
            SetTenderEarningPrices();
            return earnings;
        }

        private void SetTenderEarnings()
        {
            foreach (TenderOffer offer in offerService.GetWinningOffers())
            {
                Tender tender = tenderRepository.FindById(offer.TenderId);
                if (startDate < tender.StartDate && !tender.Opened)
                {
                    if (!IsPharmacyInEarnings(offer.PharmacyName))
                    {
                        TenderEarningDto earning = new TenderEarningDto { Name = offer.PharmacyName, Earning = 0 };
                        earnings.Add(earning);
                    }
                }
            }
        }

        private bool IsPharmacyInEarnings(string pharmacy)
        {
            foreach (TenderEarningDto earning in earnings)
            {
                if (earning.Name.Equals(pharmacy))
                {
                    return true;
                }
            }
            return false;
        }

        private void SetTenderEarningPrices()
        {
            foreach (TenderEarningDto earning in earnings)
            {
                UpdateEarning(earning);
            }
        }

        private void UpdateEarning(TenderEarningDto earning)
        {
            foreach (TenderOffer offer in offerService.GetWinningOffers())
            {
                Tender tender = tenderRepository.FindById(offer.TenderId);
                if (startDate < tender.StartDate && !tender.Opened)
                {
                    if (offer.PharmacyName.Equals(earning.Name))
                    {
                        earning.Earning += offerItemService.GetOfferPrice(offer.Id);
                    }
                }

            }
        }

        public List<TenderEarningDto> GetPharmacyOffers(string pharmacyName)
        {
            List<TenderEarningDto> pharmacyOffers = new List<TenderEarningDto>();
            foreach (TenderOffer offer in offerService.GetOffers())
            {
                if (offer.PharmacyName.Equals(pharmacyName))
                {
                    TenderEarningDto pharmacyOffer = new TenderEarningDto { Name = offer.TenderId.ToString(), Earning = offerItemService.GetOfferPrice(offer.Id) };
                    pharmacyOffers.Add(pharmacyOffer);
                }
            }
            return pharmacyOffers;
        }

        public List<TenderEarningDto> GetPharmacyWinningOffers(string pharmacyName)
        {
            List<TenderEarningDto> pharmacyOffers = new List<TenderEarningDto>();
            foreach (TenderOffer offer in offerService.GetWinningOffers())
            {
                if (offer.PharmacyName.Equals(pharmacyName))
                {
                    TenderEarningDto pharmacyOffer = new TenderEarningDto { Name = offer.TenderId.ToString(), Earning = offerItemService.GetOfferPrice(offer.Id) };
                    pharmacyOffers.Add(pharmacyOffer);
                }
            }
            return pharmacyOffers;
        }

        public int GetNumberOfPharmacyWins(string pharmacyName)
        {
            int wins = 0;
            foreach (TenderOffer offer in offerService.GetWinningOffers())
            {
                if (offer.PharmacyName.Equals(pharmacyName))
                {
                    wins++;
                }
            }
            return wins;
        }

        public int GetNumberOfPharmacyParticipations(string pharmacyName)
        {
            int participations = 0;
            tenders = new List<int>();
            foreach (TenderOffer offer in offerService.GetOffers())
            {
                if (!IsTenderChecked(offer.TenderId) && offer.PharmacyName.Equals(pharmacyName))
                {
                    participations++;
                    tenders.Add(offer.TenderId);
                }
            }
            return participations;
        }

        public List<MedicineDto> GetPharmacyMedicineConsumption(string pharmacyName)
        {
            medicineConsumption = new List<MedicineDto>();
            foreach (TenderOffer offer in offerService.GetWinningOffers())
            {
                if (offer.PharmacyName.Equals(pharmacyName))
                {
                    UpdateMedicineConsumption(offer.Id);
                }
            }
            return medicineConsumption;
        }

        private void UpdateMedicineConsumption(int offerId)
        {
            foreach (TenderOfferItemDto item in offerItemService.GetTenderOfferItems(offerId))
            {
                if (!IsMedicineInConsumption(item.Name))
                {
                    MedicineDto medicine = new MedicineDto { Name = item.Name, Quantity = item.Quantity };
                    medicineConsumption.Add(medicine);
                }
                else
                {
                    UpdateMedicineQuantity(item.Name, item.Quantity);
                }
            }
        }

        private bool IsMedicineInConsumption(string medicine)
        {
            foreach (MedicineDto med in medicineConsumption)
            {
                if (med.Name.Equals(medicine))
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateMedicineQuantity(string medicineName, int quantity)
        {
            foreach (MedicineDto med in medicineConsumption)
            {
                if (med.Name.Equals(medicineName))
                {
                    med.Quantity += quantity;
                }
            }
        }
        public void GenerateReport(PdfDocument doc)
        {
            string filePath = Directory.GetCurrentDirectory();
            string fileName = "TenderReport.pdf";

            PdfPageBase page = doc.Pages.Add();
            page.Canvas.DrawString(GetReportContent(), new PdfFont(PdfFontFamily.Helvetica, 11f), new PdfSolidBrush(Color.Black), 10, 10);


            StreamWriter File = new StreamWriter(Path.Combine(filePath, fileName), true);
            doc.SaveToStream(File.BaseStream);
            File.Close();
     
        }

        private string GetReportContent()
        {
            StringBuilder sb = new StringBuilder("\r\n\r\n");
            /*foreach (var participant in GetTendersParticipants(startDate, endDate).Select(x => x.PharmacyName))
            {
                sb.AppendLine("Pharmacy " + participant + "\r");
                sb.AppendLine("Participated on: " + GetNumberOfPharmacyParticipations(participant) + " tenders,won " + GetNumberOfPharmacyWins(participant) + " of them" + "\r\n");
                foreach (var earningDto in GetPharmaciesEarnings(startDate, endDate).Where(earningDto => earningDto.Name.Equals(participant)))
                {
                    sb.AppendLine("Total amount of profit earned: " + earningDto.Earning + "\r");
                
                }

                sb.AppendLine("------------------------------------------------------------------" + "\r");
            }*/
            return sb.ToString();
        }

        private string GetDirectory()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).ToString() + "/IntegrationAPI";
            return path;
        }

        public string GetFile()
        {
            return Path.Combine(GetDirectory(), "TenderReport.pdf");
        }

    }
}
