using IntegrationLibrary.Shared.Model;
using System.ComponentModel.DataAnnotations;

namespace IntegrationLibrary.Pharmacy.Model
{
    public class Pharmacy : Entity
    {
        
        public string PharmacyName { get; set; }
        public string PharmacyPicture { get; set; }

        public Address PharmacyAddress { get; set; }
        public ConnectionInfo PharmacyConnectionInfo { get; set; }

        public Pharmacy() { }
        public Pharmacy(int id,string pharmacyName, string picture,Address pharmacyAddress, ConnectionInfo pharmacyConnectionInfo)
        {
            Id = id;
            PharmacyName = pharmacyName;
            PharmacyPicture = picture;
            PharmacyAddress = pharmacyAddress;
            PharmacyConnectionInfo = pharmacyConnectionInfo;
        }

        public Pharmacy(PharmacyInfo info)
        {
            PharmacyName = info.PharmacyName;
            PharmacyAddress = new Address(info.Street, info.City);
            PharmacyConnectionInfo = new ConnectionInfo(info.ApiKey, info.FileProtocol, info.Url);
        }

        public void SetPharmacyPicture(string pharmacyPicture)
        {
            PharmacyPicture = pharmacyPicture;
        }
    }
}