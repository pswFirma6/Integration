using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using IntegrationLibrary.Tendering.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.Service
{
    public class TenderChartsService
    {
        private readonly ITenderOfferRepository offerRepository;
        private readonly TenderOfferService offerService;
        private readonly TenderOfferItemService tenderOfferItemService;

        private List<TenderParticipantDto> tenderParticipants;
        private List<int> tenders;

        public TenderChartsService(ITenderOfferRepository repository)
        {
            offerRepository = repository;
            offerService = new TenderOfferService(offerRepository);
            DatabaseContext context = new DatabaseContext();
            ITenderOfferItemRepository itemRepository = new TenderOfferItemRepository(context);
            tenderOfferItemService = new TenderOfferItemService(itemRepository);
        }

        public List<TenderParticipantDto> GetTendersParticipants()
        {
            tenderParticipants = new List<TenderParticipantDto>();
            SetTenderParticipants();
            foreach(TenderParticipantDto participant in tenderParticipants)
            {
                SetPharmacyParticipationNumber(participant.PharmacyName);
            }
            return tenderParticipants;
        }

        private void SetTenderParticipants() 
        {
            foreach(TenderOffer offer in offerService.GetOffers())
            {
                if (!IsPharmacyInParticipants(offer.PharmacyName))
                {
                    TenderParticipantDto participant = new TenderParticipantDto { PharmacyName = offer.PharmacyName, Participations = 0 };
                    tenderParticipants.Add(participant);
                }
            }
        }

        private bool IsPharmacyInParticipants(string pharmacy)
        {
            foreach(TenderParticipantDto participant in tenderParticipants)
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
            foreach(TenderOffer offer in offerService.GetOffers())
            {
                tenders = new List<int>();
                CheckIfTenderParticipant(offer.TenderId, pharmacy);
            }
        }

        private void CheckIfTenderParticipant(int tenderId, string pharmacy)
        {
            foreach (TenderOffer o in offerService.GetOffers())
            {
                if (o.TenderId == tenderId && !IsTenderChecked(tenderId))
                {
                    tenders.Add(tenderId);
                    if (o.PharmacyName.Equals(pharmacy))
                    {
                        UpdateParticipant(pharmacy);
                    }
                }
            }
        }

        private bool IsTenderChecked(int tenderId)
        {
            foreach(int tender in tenders)
            {
                if(tender == tenderId)
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateParticipant(string participant)
        {
            foreach(TenderParticipantDto p in tenderParticipants)
            {
                if (p.PharmacyName.Equals(participant))
                {
                    p.Participations++;
                }
            }
        }

        public List<TenderParticipantDto> GetTenderWinners()
        {
            tenderParticipants = new List<TenderParticipantDto>();
            SetTenderWinners();
            return tenderParticipants;
        }

        private void SetTenderWinners()
        {
            foreach(TenderOffer offer in offerService.GetWinningOffers())
            {
                if (!IsPharmacyInParticipants(offer.PharmacyName))
                {
                    TenderParticipantDto participant = new TenderParticipantDto { PharmacyName = offer.PharmacyName, Participations = 0 };
                    tenderParticipants.Add(participant);
                }
                UpdateParticipant(offer.PharmacyName);
            }
        }

        public List<double> GetWinningOffersPrices()
        {
            List<double> winningPrices = new List<double>();
            foreach(TenderOffer offer in offerService.GetWinningOffers())
            {
                winningPrices.Add(GetOfferPrice(offer.Id));
            }
            return winningPrices;
        }

        private double GetOfferPrice(int offerId)
        {
            List<TenderOfferItemDto> items = tenderOfferItemService.GetTenderOfferItems(offerId);
            double price = 0;
            foreach (TenderOfferItemDto item in items)
            {
                price += item.Quantity * item.Price;
            }
            return price;
        }

        //private 


    }
}
