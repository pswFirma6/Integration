using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using IntegrationLibrary.Tendering.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationLibrary.Tendering.Service
{
    public class TenderService
    {
        private readonly ITenderRepository tenderRepository;

        public TenderService(ITenderRepository iRepository)
        {
            tenderRepository = iRepository;
        }

        public List<Tender> GetTenders()
        {
            return tenderRepository.GetAll();
        }

        public void AddTender(TenderDto dto)
        {
            Tender tender = new Tender
            {
                CreationDate = DateTime.Now,
                StartDate = DateTime.Parse(dto.StartDate),
                EndDate = DateTime.Parse(dto.EndDate)
            };
            tenderRepository.Add(tender);
            tenderRepository.Save();
            AddItems(dto.TenderItems, tender.Id);
        }

        private void AddItems(List<TenderItemDto> items, int tenderId)
        {
            DatabaseContext context = new DatabaseContext();
            ITenderItemRepository repository = new TenderItemRepository(context);
            TenderItemService itemService = new TenderItemService(repository);
            itemService.AddTenderItems(SetTenderItems(items, tenderId));
        }

        private List<TenderItem> SetTenderItems(List<TenderItemDto> dtos, int tenderId)
        {
            List<TenderItem> items = new List<TenderItem>();
            foreach(TenderItemDto dto in dtos)
            {
                TenderItem item = new TenderItem()
                {
                    Name = dto.Name,
                    Quantity = dto.Quantity,
                    TenderId = tenderId
                };
                items.Add(item);
            }
            return items;
        }

    }
}
