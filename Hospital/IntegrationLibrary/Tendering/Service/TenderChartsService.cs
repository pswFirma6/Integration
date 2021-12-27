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
        private List<TenderParticipantDto> tenderParticipants;
        private List<int> tenders;

        public TenderChartsService()
        {
            DatabaseContext context = new DatabaseContext();
            offerRepository = new TenderOfferRepository(context);
            offerService = new TenderOfferService(offerRepository);
        }

        public List<TenderParticipantDto> GetTendersParticipants()    // potrebna lista imena apoteka sa brojem ucestvovanja
        {
            tenderParticipants = new List<TenderParticipantDto>();
            SetTenderParticipants();
            foreach(TenderParticipantDto participant in tenderParticipants)
            {
                SetPharmacyParticipationNumber(participant.PharmacyName);
            }
            return tenderParticipants;
        }

        private void SetTenderParticipants() // imena svih apoteka koje su bar jednom ucestvovale
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
            foreach(TenderParticipantDto participant in tenderParticipants)
            {
                SetPharmacyWinnersNumber(participant.PharmacyName);
            }
            return tenderParticipants;
        }

        private void SetTenderWinners()
        {
            foreach (TenderOffer offer in offerService.GetOffers())
            {
                if (!IsPharmacyInParticipants(offer.PharmacyName) && offer.isWinner)
                {
                    TenderParticipantDto participant = new TenderParticipantDto { PharmacyName = offer.PharmacyName, Participations = 0 };
                    tenderParticipants.Add(participant);
                }
            }
        }

        private void SetPharmacyWinnersNumber(string pharmacy)
        {
            foreach(TenderOffer offer in offerService.GetOffers())
            {
                if (offer.isWinner && offer.PharmacyName.Equals(pharmacy))
                {
                    UpdateParticipant(pharmacy);
                }
            }
        }


    }
}
