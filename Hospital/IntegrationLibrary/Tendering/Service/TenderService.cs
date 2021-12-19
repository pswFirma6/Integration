using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using IntegrationLibrary.Tendering.Repository;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationLibrary.Tendering.Service
{
    public class TenderService
    {
        private readonly ITenderRepository tenderRepository;
        private readonly TenderItemService tenderItemService;

        public TenderService(ITenderRepository iRepository)
        {
            tenderRepository = iRepository;
            DatabaseContext context = new DatabaseContext();
            ITenderItemRepository itemRepository = new TenderItemRepository(context);
            tenderItemService = new TenderItemService(itemRepository);
        }

        public List<Tender> GetTenders()
        {
            return tenderRepository.GetAll();
        }

        public List<TenderDto> GetTendersWithItems()
        {
            List<TenderDto> tendersWithItems = new List<TenderDto>();
            foreach(Tender tender in GetTenders())
            {
                TenderDto dto = new TenderDto
                {
                    Id = tender.Id,
                    CreationDate = tender.CreationDate.ToString(),
                    StartDate = tender.StartDate.ToString(),
                    EndDate = tender.EndDate.ToString(),
                    TenderItems = tenderItemService.GetTenderItems(tender.Id)
                };
                tendersWithItems.Add(dto);
            }
            return tendersWithItems;
        }

        public void AddTender(TenderDto dto, string apiKey)
        {
            var factory = new ConnectionFactory
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest",
                Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest",
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "tender-exchange", type: ExchangeType.Fanout);

                dto.Id = tenderRepository.GetAll().Count;
                var message = dto;
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("tender-exchange", String.Empty, null, body);
            }

            Tender tender = new Tender
            {
                CreationDate = DateTime.Now,
                StartDate = DateTime.Parse(dto.StartDate),
                EndDate = AssignEndDate(dto.EndDate)
            };
            tenderRepository.Add(tender);
            tenderRepository.Save();
            tenderItemService.AddTenderItems(SetTenderItems(dto.TenderItems, tender.Id));
        }

        private DateTime AssignEndDate(string endDate)
        {
            return string.IsNullOrEmpty(endDate) ? new DateTime(2050, 01, 01) : DateTime.Parse(endDate);
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
