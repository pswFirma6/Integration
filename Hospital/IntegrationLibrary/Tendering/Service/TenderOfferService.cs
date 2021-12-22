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

        public List<TenderOffer> GetTenderOffers()
        {
            return tenderOfferRepository.GetAll();
        }

        public List<TenderOfferDto> GetTendersWithItems()
        {
            List<TenderOfferDto> tenderOffersWithItems = new List<TenderOfferDto>();
            foreach (TenderOffer tenderOffer in GetTenderOffers())
            {
                TenderOfferDto dto = new TenderOfferDto
                {
                    Id = tenderOffer.Id,
                    TenderId = tenderOffer.TenderId,
                    PharmacyName = tenderOffer.PharmacyName,
                    TenderOfferItems = tenderOfferItemService.GetTenderOfferItems(tenderOffer.Id)
                };
                tenderOffersWithItems.Add(dto);
            }
            return tenderOffersWithItems;
        }

        public void AddTenderOffer(TenderOfferDto dto)
        {
            TenderOffer tenderOffer = new TenderOffer
            {
                Id = dto.Id,
                TenderId = dto.TenderId,
                PharmacyName = dto.PharmacyName
            };
            tenderOfferRepository.Add(tenderOffer);
            tenderOfferRepository.Save();
            tenderOfferItemService.AddTenderOfferItems(SetTenderOfferItems(dto.TenderOfferItems, tenderOffer.Id));
        }

        public void MakeOfferWinner(TenderOffer offer)
        {
            offer.isWinner = true;
            tenderOfferRepository.Update(offer);
            tenderOfferRepository.Save();
        }

        private List<TenderOfferItem> SetTenderOfferItems(List<TenderOfferItemDto> dtos, int tenderOfferId)
        {
            List<TenderOfferItem> items = new List<TenderOfferItem>();
            foreach (TenderOfferItemDto dto in dtos)
            {
                TenderOfferItem item = new TenderOfferItem()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Quantity = dto.Quantity,
                    Price = dto.Price,
                    TenderOfferId = tenderOfferId
                };
                items.Add(item);
            }
            return items;
        }



    }
}
