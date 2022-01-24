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
    public class TenderOfferService
    {
        private readonly ITenderOfferRepository tenderOfferRepository;
        private readonly TenderOfferItemService tenderOfferItemService;

        public TenderOfferService(ITenderOfferRepository iRepository)
        {
            tenderOfferRepository = iRepository;
            DatabaseContext context = new DatabaseContext();
            ITenderOfferItemRepository itemRepository = new TenderOfferItemRepository(context);
            tenderOfferItemService = new TenderOfferItemService(itemRepository);
        }

        public List<TenderOffer> GetOffers()
        {
            return tenderOfferRepository.GetTenderOffers();
        }

        public List<TenderOfferDto> GetTenderOffersWithItems()
        {
            List<TenderOfferDto> tenderOffersWithItems = new List<TenderOfferDto>();
            foreach (TenderOffer tenderOffer in GetOffers())
            {
                TenderOfferDto dto = new TenderOfferDto
                {
                    Id = tenderOffer.Id,
                    TenderId = tenderOffer.TenderId,
                    PharmacyName = tenderOffer.PharmacyName,
                    //TenderOfferItems = tenderOfferItemService.GetTenderOfferItems(tenderOffer.Id)
                    TenderOfferItems = GetOfferItems(tenderOffer)
                };
                tenderOffersWithItems.Add(dto);
            }
            return tenderOffersWithItems;
        }

        private List<TenderOfferItemDto> GetOfferItems(TenderOffer offer)
        {
            List<TenderOfferItemDto> items = new List<TenderOfferItemDto>();
            foreach(TenderOfferItem offerItem in offer.OfferItems)
            {
                TenderOfferItemDto itemDto = new TenderOfferItemDto
                {
                    Name = offerItem.Name,
                    Quantity = offerItem.Quantity,
                    Price = offerItem.Price
                };
                items.Add(itemDto);
            }
            return items;
        }

        public void AddTenderOffer(TenderOfferDto dto)
        {
            TenderOffer tenderOffer = new TenderOffer
            {
                Id = GetLastID() + 1,
                TenderId = dto.TenderId,
                PharmacyName = dto.PharmacyName,
                PharmacyOfferId = dto.Id
            };
            SetOfferItems(dto.TenderOfferItems, tenderOffer);
            tenderOfferRepository.Add(tenderOffer);
            tenderOfferRepository.Save();
        }

        private TenderOffer SetOfferItems(List<TenderOfferItemDto> items, TenderOffer offer)
        {
            foreach(TenderOfferItemDto dto in items)
            {
                offer.AddOfferItem(offer, dto.Name, dto.Quantity, dto.Price);
            }
            return offer;
        }

        private int GetLastID()
        {
            List<TenderOffer> offers = GetOffers();
            if (offers.Count == 0)
            {
                return 0;
            }
            return offers[offers.Count - 1].Id;
        }

        public void MakeOfferWinner(TenderOffer offer)
        {
            offer.IsWinner = true;
            tenderOfferRepository.Update(offer);
            tenderOfferRepository.Save();
        }

        public List<TenderOffer> GetWinningOffers()
        {
            List<TenderOffer> winningOffers = new List<TenderOffer>();
            foreach(TenderOffer offer in GetOffers())
            {
                if (offer.IsWinner)
                    winningOffers.Add(offer);
            }
            return winningOffers;
        }

    }
}
