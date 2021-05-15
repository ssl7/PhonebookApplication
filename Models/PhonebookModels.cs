using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookApplication.Models
{
    public class PhonebookRecordModel
    {
        [Key]
        public int Id { get; set; }
       
        [StringLength(128)]
        public string FirstName { get; set; }
       
        [StringLength(128)]
        public string LastName { get; set; }
       
        [StringLength(512)]
        public string StreetAddress { get; set; }
        
        [StringLength(128)]
        public string City { get; set; }
       
        [StringLength(16)]
        public string ZipCode { get; set; }
        
        [StringLength(16)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
       
        [StringLength(128)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class PhonebookRequestModel
    {
        public string FirstName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string SortOrder { get; set; } = "firstName";
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
    }
    public class PhonebookResponseModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<PhonebookRecordModel> Records { get; set; }
    }
    public class PhonebookRecordAddResponseModel
    {
        public PhonebookRecordModel Record { get; set; }
        public List<ValidationFailure> ValidationErrors { get; set; }
    }
}
