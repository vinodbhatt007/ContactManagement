using ContactManagement.DataContract.DAL.Repository;
using ContactManagement.DataContract.DTO;
using ContactManagement.DataContracts.Infrastructure;
using System.Linq;

namespace ContactManagement.DataContracts.API
{
    public class ContactDetailManager
    {

        public IQueryable<ContactDetailDTO> GetAllContacts()
        {
            using (ContactManagementRepository repo = new ContactManagementRepository())
            {
                return repo.GetAllContacts();
            }
        }

        public ContactDetailDTO GetContactById(int Id)
        {
            using (ContactManagementRepository repo = new ContactManagementRepository())
            {
                return repo.GetContactById(Id);
            }
        }

        public SaveChangeEnum AddContact(ContactDetailDTO _ContactDetailDTO)
        {
            using (ContactManagementRepository repo = new ContactManagementRepository())
            {
                return repo.AddContact(_ContactDetailDTO);
            }
        }

        public SaveChangeEnum UpdateContact(ContactDetailDTO _ContactDetailDTO)
        {
            using (ContactManagementRepository repo = new ContactManagementRepository())
            {
                return repo.UpdateContact(_ContactDetailDTO);
            }
        }

        public SaveChangeEnum DeleteContact(int Id)
        {
            using (ContactManagementRepository repo = new ContactManagementRepository())
            {
                return repo.DeleteContact(Id);
            }
        }
    }
}
