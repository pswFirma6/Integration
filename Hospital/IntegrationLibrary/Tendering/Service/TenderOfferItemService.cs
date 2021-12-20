using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.Service
{
    public class TenderOfferItemService
    {
        private readonly ITenderOfferItemRepository tenderOfferItemRepository;

        public TenderOfferItemService(ITenderOfferItemRepository iRepository)
        {
            tenderOfferItemRepository = iRepository;
        }

        public List<TenderOfferItem> GetAll()
        {
            return tenderOfferItemRepository.GetAll();
        }

        public List<TenderOfferItemDto> GetTenderOfferItems(int tenderId)
        {
            List<TenderOfferItemDto> offerItems = new List<TenderOfferItemDto>();
            foreach (TenderOfferItem item in GetAll())
            {
                if (item.Id == tenderId)
                {
                    TenderOfferItemDto dto = new TenderOfferItemDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };
                    offerItems.Add(dto);
                }
            }
            return offerItems;
        }

        public void AddTenderOfferItems(List<TenderOfferItem> tenderOfferItems)
        {
            foreach (TenderOfferItem item in tenderOfferItems)
            {
                item.Id = tenderOfferItemRepository.GetAll().Count + 1;
                tenderOfferItemRepository.Add(item);
                tenderOfferItemRepository.Save();
            }
        }

    }
}
