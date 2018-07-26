using ContactManagement.DataContract.DAL.Context;
using ContactManagement.DataContract.DAL.Context.Tables;
using ContactManagement.DataContract.DTO;
using ContactManagement.DataContracts.DAL.Repository;
using ContactManagement.DataContracts.Infrastructure;
using System;
using System.Linq;

namespace ContactManagement.DataContract.DAL.Repository
{
    class ContactManagementRepository : GenericRepository
    {
        private new readonly ContactManagementContext _Context;
        ContactManagementContext Model { get { return _Context; } }

        public ContactManagementRepository(string connectionStringName): base(connectionStringName)
        {
        }

        public ContactManagementRepository(): base(new ContactManagementContext())
        {
            this._Context = (ContactManagementContext)base._Context;
        }

        public ContactManagementRepository(ContactManagementContext context): base(context)
        {
            this._Context = context;
        }

        public ContactManagementRepository(ContactManagementContext context, bool lazyLoading): base(context, lazyLoading: lazyLoading)
        {
            this._Context = context;
        }


        public IQueryable<ContactDetailDTO> GetAllContacts()
        {
            try
            {
                var res =  (from ContactDetail in this.Model.ContactDetail
                        select new ContactDetailDTO
                        {
                            Id = ContactDetail.Id,
                            FirstName = ContactDetail.FirstName,
                            LastName = ContactDetail.LastName,
                            Email = ContactDetail.Email,
                            PhoneNumber = ContactDetail.PhoneNumber,
                            Status = ContactDetail.Status
                        }).ToList().AsQueryable().OrderBy(x => x.Id);

                return res;
            }
            catch (Exception)
            {
                return null;
            }

            
        }

        public ContactDetailDTO GetContactById(int _Id)
        {
            try
            {
                return (from ContactDetails in this.Model.ContactDetail
                        where ContactDetails.Id == _Id
                        select new ContactDetailDTO
                        {
                            Id = ContactDetails.Id,
                            FirstName = ContactDetails.FirstName,
                            LastName = ContactDetails.LastName,
                            Email = ContactDetails.Email,
                            PhoneNumber = ContactDetails.PhoneNumber,
                            Status = ContactDetails.Status
                        }).FirstOrDefault();
            }
            catch (Exception)
            {
               return null;
            }
        }

        public SaveChangeEnum AddContact(ContactDetailDTO _ContactDetailDTO)
        {
            SaveChangeEnum retVal = SaveChangeEnum.No_Action;
            try
            {
                this.UnitOfWork.BeginTransaction();
                 ContactDetail _ContactDetail = new ContactDetail();
                _ContactDetail.FirstName = _ContactDetailDTO.FirstName;
                _ContactDetail.LastName = _ContactDetailDTO.LastName;
                _ContactDetail.Email = _ContactDetailDTO.Email;
                _ContactDetail.PhoneNumber = _ContactDetailDTO.PhoneNumber;
                _ContactDetail.Status = _ContactDetailDTO.Status;
                this.Add<ContactDetail>(_ContactDetail);
                this._Context.SaveChanges();
                retVal = this.UnitOfWork.CommitTransaction();

            }
            catch (Exception)
            {
                //Log Errors
            }
            
            return retVal;
        }

        public SaveChangeEnum UpdateContact(ContactDetailDTO _ContactDetailDTO)
        {
            SaveChangeEnum retVal = SaveChangeEnum.No_Action;

            try
            {
                ContactDetail _ContactDetail = (from cd in this.Model.ContactDetail
                                                where cd.Id == _ContactDetailDTO.Id
                                                select cd).FirstOrDefault();

                if (_ContactDetail != null)
                {
                    this.UnitOfWork.BeginTransaction();
                    _ContactDetail.FirstName = _ContactDetailDTO.FirstName;
                    _ContactDetail.LastName = _ContactDetailDTO.LastName;
                    _ContactDetail.Email = _ContactDetailDTO.Email;
                    _ContactDetail.PhoneNumber = _ContactDetailDTO.PhoneNumber;
                    _ContactDetail.Status = _ContactDetailDTO.Status;
                    this.Update<ContactDetail>(_ContactDetail);
                    this._Context.SaveChanges();
                    retVal = this.UnitOfWork.CommitTransaction();

                }
            }
            catch (Exception)
            {

            }
           

            return retVal;
        }

        public SaveChangeEnum DeleteContact(int _Id)
        {
            SaveChangeEnum retVal = SaveChangeEnum.No_Action;
            try
            {
                ContactDetail ContactDetail = (from ContactDetails in this.Model.ContactDetail
                                               where ContactDetails.Id == _Id
                                               select ContactDetails).FirstOrDefault();

                if (ContactDetail != null)
                {
                    this.UnitOfWork.BeginTransaction();
                    this.Delete<ContactDetail>(ContactDetail);
                    this._Context.SaveChanges();
                    retVal = this.UnitOfWork.CommitTransaction();
                }
            }
            catch (Exception)
            {
                //Log Errors
            }

            return retVal;
        }
    }
}
