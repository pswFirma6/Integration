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
    public class TenderService
    {
        private readonly ITenderRepository tenderRepository;

        public TenderService(ITenderRepository iRepository)
        {
            tenderRepository = iRepository;
        }

        public void addTender(TenderDTO dto)
        {
            Tender tender = new Tender();
            tender.CreationDate = DateTime.Now;
            tender.StartDate = DateTime.Parse(dto.StartDate);
            tender.EndDate = DateTime.Parse(dto.EndDate);
            tenderRepository.Add(tender);
            //addItems(dto.TenderItems);
        }


        private void addItems(List<TenderItem> items)
        {
            DatabaseContext context = new DatabaseContext();
            ITenderItemRepository repository = new TenderItemRepository(context);
            TenderItemService itemService = new TenderItemService(repository);
            itemService.addTenderItems(items);
        }

    }
}
