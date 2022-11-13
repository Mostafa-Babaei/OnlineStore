using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Model;
using Application.Model;
using Domain.Entities;

namespace infrastructure.Service
{
    public interface IContactService
    {
        ApiResult InsertContact(CreateContactDto model);
        IEnumerable<Contact> GetAllContact();
        List<ContactPageDto> GetHomeContacts();
        Contact GetContact(int id);
        List<Contact> GetUnreadContact();
        PagingDto<Contact> SearchContact(int page, int pageSize);
        ApiResult ReadContact();
        ApiResult DeleteContact();
        ApiResult ResponseContact();
    }
}
