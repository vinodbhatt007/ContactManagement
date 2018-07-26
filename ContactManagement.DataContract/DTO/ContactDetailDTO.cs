using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.DataContract.DTO
{
    public class ContactDetailDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
    }
}
