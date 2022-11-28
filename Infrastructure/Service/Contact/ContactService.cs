using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Model;
using Application.Model;
using Domain.Entities;
using infrastructure.Repository;

namespace infrastructure.Service
{
    public class ContactService : IContactService
    {
        private IGenericRepository<Contact> contactRepository;
        private readonly IMapper mapper;

        public ContactService(IGenericRepository<Contact> contactRepository, IMapper mapper)
        {
            this.contactRepository = contactRepository;
            this.mapper = mapper;
        }
        public ApiResult DeleteContact()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> GetAllContact()
        {
            return contactRepository.GetAll();
        }

        public Contact GetContact(int id)
        {
            return contactRepository.GetById(id);
        }

        public List<ContactPageDto> GetHomeContacts()
        {
            var model = contactRepository.Find(e => e.ShowInHome).ToList();
            List<ContactPageDto> result = new List<ContactPageDto>();
            if (model.Any())
            {
                foreach (var item in model)
                    result.Add(mapper.Map<ContactPageDto>(item));
            }
            return result;
        }

        public List<Contact> GetUnreadContact()
        {
            throw new NotImplementedException();
        }

        public ApiResult InsertContact(CreateContactDto model)
        {

            Contact contact = mapper.Map<Contact>(model);
            contact.CreateAt = DateTime.Now;
            contactRepository.Add(contact);
            int result = contactRepository.SaveEntity();
            if (result > 0)
                return ApiResult.ToSuccessModel("پیام شما با موفقیت ارسال شد");

            return ApiResult.ToErrorModel("خطا در ارسال پیام ، لطفا مجدد تلاش نمایید");
        }

        public ApiResult ReadContact()
        {
            throw new NotImplementedException();
        }

        public ApiResult ResponseContact()
        {
            throw new NotImplementedException();
        }

        public PagingDto<Contact> SearchContact(int page, int pageSize)
        {
            return contactRepository.GetWithPaging(null, new PagingDto<Contact>() { Page = page, PageSize = pageSize });
        }

    }
}
