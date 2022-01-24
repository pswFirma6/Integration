using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using IntegrationLibrary.Tendering.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationLibrary.Tendering.Service
{
    public class TenderService
    {
        private readonly ITenderRepository tenderRepository;
        private readonly string url = "http://localhost:44377/tenderNotification";
        public TenderService(ITenderRepository iRepository)
        {
            tenderRepository = iRepository;
        }

        public List<Tender> GetTenders()
        {
            return tenderRepository.GetTenders();
        }

        public List<TenderDto> GetTendersWithItems()
        {
            List<TenderDto> tendersWithItems = new List<TenderDto>();
            foreach (Tender tender in GetTenders())
            {
                if (tender.Opened)
                {
                    TenderDto dto = new TenderDto
                    {
                        Id = tender.Id,
                        CreationDate = tender.CreationDate.ToString(),
                        StartDate = tender.TenderDateRange.StartDate.ToString(),
                        EndDate = tender.TenderDateRange.EndDate.ToString(),
                        TenderItems = GetTenderItems(tender)
                    };
                    tendersWithItems.Add(dto);
                }
            }
            return tendersWithItems;
        }

        private List<TenderItemDto> GetTenderItems(Tender tender)
        {
            List<TenderItemDto> items = new List<TenderItemDto>();
            foreach(TenderItem tenderItem in tender.TenderItems){
                TenderItemDto itemDto = new TenderItemDto { Name = tenderItem.Name, Quantity = tenderItem.Quantity };
                items.Add(itemDto);
            }
            return items;
        }

        public void AddTender(TenderDto dto)
        {
            Tender tender = new Tender
            {
                Id = GetLastID() + 1,
                Opened = true,
                CreationDate = DateTime.Now,
                TenderDateRange = new Shared.Model.DateRange 
                { 
                    StartDate = DateTime.Parse(dto.StartDate), 
                    EndDate = AssignEndDate(dto.EndDate)
                }
                
            };
            SetTenderItems(dto.TenderItems, tender);
            tenderRepository.Add(tender);
            tenderRepository.Save();

            var factory = new ConnectionFactory
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest",
                Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest",
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "tender-exchange-" + dto.HospitalApiKey, type: ExchangeType.Fanout);

                TenderForPharmacyDto tenderDto = new TenderForPharmacyDto
                {
                    Id = tender.Id,
                    CreationDate = tender.CreationDate.ToString(),
                    StartDate = tender.TenderDateRange.StartDate.ToString(),
                    EndDate = tender.TenderDateRange.EndDate.ToString(),
                    TenderItems = dto.TenderItems,
                    HospitalApiKey = dto.HospitalApiKey,
                    HospitalTenderId = tender.Id,
                    Opened = true
                };
                var message = tenderDto;
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("tender-exchange-" + dto.HospitalApiKey, String.Empty, null, body);
            }
        }

        public void SendNotification(TenderOffer offer)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddJsonBody(offer);
            client.Post(request);
        }

        private int GetLastID()
        {
            List<Tender> tenders = GetTenders();
            if(tenders.Count == 0)
            {
                return 0;
            }
            return tenders[tenders.Count - 1].Id;
        }

        private DateTime AssignEndDate(string endDate)
        {
            return string.IsNullOrEmpty(endDate) ? new DateTime(2050, 01, 01) : DateTime.Parse(endDate);
        }

        private Tender SetTenderItems(List<TenderItemDto> dtos, Tender tender)
        {
            foreach (TenderItemDto dto in dtos)
            {
                tender.AddTenderItem(tender, dto.Name, dto.Quantity);
            }
            return tender;
        }

        public void CloseTender(int tenderId)
        {
            Tender tender = GetTenders().Find(tender => tenderId == tender.Id);
            tender.Opened = false;
            tenderRepository.Update(tender);
            tenderRepository.Save();
        }



    }
}
