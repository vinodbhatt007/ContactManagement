using ContactManagement.DataContract.DTO;
using ContactManagement.DataContracts.API;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ContactManagement.WebAPI.Models
{

    public class ContactDetailModel
    {

        public int Id { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length is '50'")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length is '50'")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]

        [MaxLength(50, ErrorMessage = "Max Length is '50'")]
        public string Email { get; set; }
        [Display(Name ="Phone Number")]
        [MaxLength(10, ErrorMessage = "Max Length is '10'")]
        [Required(ErrorMessage = "Phone Number is Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number is not valid")]
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }


        ContactDetailManager _ContactDetailManager = new ContactDetailManager();

        //Get All Contact Details
        public List<ContactDetailModel> GetAllContacts()
        {
            IQueryable<ContactDetailDTO> _ListContactDetailDTO = _ContactDetailManager.GetAllContacts();
            List<ContactDetailModel> _ContactDetailList = new List<ContactDetailModel>();
            foreach (var _ContactDetailDTO in _ListContactDetailDTO)
            {
                ContactDetailModel _ContactDetailModel = new ContactDetailModel();
                _ContactDetailModel.FirstName = _ContactDetailDTO.FirstName;
                _ContactDetailModel.LastName = _ContactDetailDTO.LastName;
                _ContactDetailModel.Id = _ContactDetailDTO.Id;
                _ContactDetailModel.Email = _ContactDetailDTO.Email;
                _ContactDetailModel.PhoneNumber = _ContactDetailDTO.PhoneNumber;
                _ContactDetailModel.Status = _ContactDetailDTO.Status;

                _ContactDetailList.Add(_ContactDetailModel);
            }

            return _ContactDetailList;
        }


        //Get Selected Contact Detail
        public ContactDetailModel GetContactById(int _Id)
        {
            ContactDetailDTO _ContactDetailDTO = _ContactDetailManager.GetContactById(_Id);
            ContactDetailModel _ContactDetailModel = new ContactDetailModel();
            _ContactDetailModel.FirstName = _ContactDetailDTO.FirstName;
            _ContactDetailModel.LastName = _ContactDetailDTO.LastName;
            _ContactDetailModel.Id = _ContactDetailDTO.Id;
            _ContactDetailModel.Email = _ContactDetailDTO.Email;
            _ContactDetailModel.PhoneNumber = _ContactDetailDTO.PhoneNumber;
            _ContactDetailModel.Status = _ContactDetailDTO.Status;

            return _ContactDetailModel;

        }

        // Update Contact
        public string UpdateContact(ContactDetailModel _ContactDetailModel)
        {
            ContactDetailDTO _ContactDetailDTO = new ContactDetailDTO();
            _ContactDetailDTO.Id = _ContactDetailModel.Id;
            _ContactDetailDTO.FirstName = _ContactDetailModel.FirstName;
            _ContactDetailDTO.LastName = _ContactDetailModel.LastName;
            _ContactDetailDTO.Email = _ContactDetailModel.Email;
            _ContactDetailDTO.PhoneNumber = _ContactDetailModel.PhoneNumber;
            _ContactDetailDTO.Status = _ContactDetailModel.Status;

            return _ContactDetailManager.UpdateContact(_ContactDetailDTO).ToString();
        }

        //Create a new Contact
        public string CreateContact(ContactDetailModel _ContactDetailModel)
        {
            ContactDetailDTO _ContactDetailDTO = new ContactDetailDTO();
            _ContactDetailDTO.FirstName = _ContactDetailModel.FirstName;
            _ContactDetailDTO.LastName = _ContactDetailModel.LastName;
            _ContactDetailDTO.Id = _ContactDetailModel.Id;
            _ContactDetailDTO.Email = _ContactDetailModel.Email;
            _ContactDetailDTO.PhoneNumber = _ContactDetailModel.PhoneNumber;
            _ContactDetailDTO.Status = true;

            return _ContactDetailManager.AddContact(_ContactDetailDTO).ToString();

        }

        //Delete Contact
        public string DeleteContact(int _Id)
        {
            return _ContactDetailManager.DeleteContact(_Id).ToString();

        }

    }
}