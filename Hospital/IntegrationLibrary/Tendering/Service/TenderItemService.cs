using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.Service
{
    public class TenderItemService
    {
        private readonly ITenderItemRepository tenderItemRepository;

        public TenderItemService(ITenderItemRepository iRepository)
        {
            tenderItemRepository = iRepository;
        }

        public List<TenderItem> GetAll()
        {
            return tenderItemRepository.GetAll();
        }

        public List<TenderItemDto> GetTenderItems(int tenderId)
        {
            List<TenderItemDto> items = new List<TenderItemDto>();
            foreach(TenderItem item in GetAll())
            {
                if(item.TenderId == tenderId)
                {
                    TenderItemDto dto = new TenderItemDto
                    {
                        
                        Name = item.Name, 
                        Quantity = item.Quantity
                    };
                    items.Add(dto);
                }
            }
            return items;
        }

        public void AddTenderItems(List<TenderItem> items)
        {
            foreach(TenderItem item in items)
            {
                item.Id = GetLastID() + 1;
                tenderItemRepository.Add(item);
                tenderItemRepository.Save();
            }
        }

        private int GetLastID()
        {
            List<TenderItem> items = GetAll();
            return items[items.Count - 1].Id;
        }
    }
}
