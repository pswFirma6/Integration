﻿using IntegrationLibrary.Tendering.DTO;
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

        public void addTenderItems(List<TenderItem> items)
        {
            foreach(TenderItem item in items)
            {
                tenderItemRepository.Add(item);
            }
        }
    }
}
