using ContactManagement.WebAPI.Models;
using System;
using System.Web.Http;

namespace ContactManagement.WebAPI.Controllers
{
    [RoutePrefix("api/contacts")]
    public class ContactDetailController : ApiController
    {
        private readonly ContactDetailModel _ContactDetailModel;

        public ContactDetailController()
        {
            _ContactDetailModel = new ContactDetailModel();
        }



        #region Action Methods
    
        [HttpPost]
        [Route("CreateContact")]
        public IHttpActionResult CreateContact([FromBody] ContactDetailModel _ContactDetail)
        {
            try
            {
                if (_ContactDetail == null)
                    return BadRequest();

                if (_ContactDetailModel.CreateContact(_ContactDetail) == "Save_Successfull")
                {
                    return Ok(true);
                }            

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(false);
        }

        [HttpPost]
        [Route("UpdateContact")]
        public IHttpActionResult UpdateContact([FromBody] ContactDetailModel _ContactDetail)
        {
            try
            {
                if (_ContactDetail != null)
                {
                    _ContactDetailModel.UpdateContact(_ContactDetail);
                    return Ok(true);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPost]
        [Route("DeleteContact")]
        public IHttpActionResult DeleteContact([FromBody] ContactDetailModel _ContactDetail)
        {
            try
            {
                if (_ContactDetail.Id > 0)
                {
                    _ContactDetailModel.DeleteContact(_ContactDetail.Id);
                    return Ok(true);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }


        [HttpPost]
        [Route("GetContactById")]
        public IHttpActionResult GetContact([FromBody] ContactDetailModel _ContactDetail)
        {
            try
            {
                if (_ContactDetail.Id > 0)
                {
                    var contact = _ContactDetailModel.GetContactById(_ContactDetail.Id);

                    if (contact != null)
                        return Ok(contact);
                    else
                        return NotFound();
                }
                else
                {
                    return BadRequest("The Contact Id is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }


        [HttpPost]
        [Route("GetAllContacts")]
        public IHttpActionResult GetAllContacts()
        {
            try
            {
                var allContacts = _ContactDetailModel.GetAllContacts();
                return Ok(allContacts);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }


        #endregion
    }
}
