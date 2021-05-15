using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PagedList;
using PhonebookApplication.Models;
using PhonebookApplication.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookApplication.Repository
{
    public class PhonebookRepository
    {
        private readonly ApplicationDbContext _db;

        public PhonebookRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<PhonebookResponseModel> GetsAsync(PhonebookRequestModel request)
        {

            //Filtering
            var res = _db.Phonebook.Where(f =>
                (f.FirstName == request.FirstName || request.FirstName == null) &&
                (f.City == request.City || request.City == null) &&
                (f.ZipCode == request.ZipCode || request.ZipCode == null) &&
                (f.PhoneNumber == request.PhoneNumber || request.PhoneNumber == null));

            //Sorting
            if (request.SortOrder == "firstName")
                res = res.OrderBy(o => o.FirstName);
            if (request.SortOrder == "firstName_Desc")
                res = res.OrderByDescending(o => o.FirstName);

            //Paging
            var paggingRes = res.ToPagedList(request.PageNumber, request.PageSize);

            return new PhonebookResponseModel
            {
                Records = paggingRes.ToList(),
                TotalItems = paggingRes.TotalItemCount,
                CurrentPage = paggingRes.PageNumber,
                PageSize = paggingRes.Count,
                TotalPages = paggingRes.PageCount
            };

        }

        public async Task<PhonebookRecordModel> GetAsync(int id)
        {
            return await _db.Phonebook.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _db.Phonebook.FindAsync(id);
            if (record == null)
                return false;

            _db.Phonebook.Remove(record);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<PhonebookRecordAddResponseModel> AddAsync(PhonebookRecordModel record)
        {
            var res = new PhonebookRecordAddResponseModel();
            var validationResults = new PhonebookRecordValidator().Validate(record);
            
            res.ValidationErrors = validationResults.Errors;

            if (validationResults.IsValid)
            {
                _db.Phonebook.Add(record);
                await _db.SaveChangesAsync();
                res.Record = record;
            }

            return res;
        }
    }
}
