using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace ContactManagement.WebAPI.Models
{

    public class APIClient
    {
        private readonly string BaseURI = Common.Consumable.WebAddress.GetWebAddress();

        public IEnumerable<ContactDetailModel> GetAllContacts()
        {
            IEnumerable<ContactDetailModel> _ListContactDetailModel = null;
            try
            {
                WebClient client = new WebClient();
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                client.Headers.Add("Content-Type:application/json");
                string apiUrl = BaseURI + "api/contacts/GetAllContacts";

                client.Headers.Add("Accept:application/json");

                var response = client.UploadString(apiUrl, "POST");
                _ListContactDetailModel = JsonConvert.DeserializeObject<IEnumerable<ContactDetailModel>>(response);
            }

            catch
            {
                //Log Errors
            }

            return _ListContactDetailModel;
        }

        public ContactDetailModel GetContactById(int _Id)
        {
            ContactDetailModel _ContactDetailModel = null;
            try
            {
                WebClient client = new WebClient();
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                client.Headers.Add("Content-Type:application/json");
                string apiUrl = BaseURI + "api/contacts/GetContactById";
                client.Headers.Add("Accept:application/json");
                ContactDetailModel model = new ContactDetailModel();
                model.Id = _Id;
                String objectName = JsonConvert.SerializeObject(model);
                var response = client.UploadString(apiUrl, "POST", objectName);
                _ContactDetailModel = JsonConvert.DeserializeObject<ContactDetailModel>(response);
            }
            catch
            {
                //Log Errors
            }

            return _ContactDetailModel;
        }

        public bool CreateContact(ContactDetailModel _ContactDetailModel)
        {
            bool retVal = false;
            try
            {
                WebClient client = new WebClient();
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                client.Headers.Add("Content-Type:application/json");
                string apiUrl = BaseURI + "api/contacts/CreateContact";
                client.Headers.Add("Accept:application/json");
                String objectName = JsonConvert.SerializeObject(_ContactDetailModel);
                var response = client.UploadString(apiUrl, "POST", objectName);
                if (response == "true")
                {
                    retVal = true;
                }
            }
            catch
            {
                //Log Errors
            }

            return retVal;
        }

        public bool UpdateContact(ContactDetailModel _ContactDetailModel)
        {
            bool retVal = false;
            try
            {
                WebClient client = new WebClient();
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                client.Headers.Add("Content-Type:application/json");
                string apiUrl = BaseURI + "api/contacts/UpdateContact";
                client.Headers.Add("Accept:application/json");
                String objectName = JsonConvert.SerializeObject(_ContactDetailModel);
                var response = client.UploadString(apiUrl, "POST", objectName);
                if (response == "true")
                {
                    retVal = true;
                }
            }
            catch
            {
                //Log Errors
            }

            return retVal;
        }

        public bool DeleteContact(int _Id)
        {
            bool retVal = false;
            try
            {
                WebClient client = new WebClient();
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                client.Headers.Add("Content-Type:application/json");
                string apiUrl = BaseURI + "api/contacts/DeleteContact";
                client.Headers.Add("Accept:application/json");
                ContactDetailModel model = new ContactDetailModel();
                model.Id = _Id;
                String objectName = JsonConvert.SerializeObject(model);
                var response = client.UploadString(apiUrl, "POST", objectName);
                if (response == "true")
                {
                    retVal = true;
                }
            }
            catch
            {
                //Log Errors
            }

            return retVal;
        }
    }
}