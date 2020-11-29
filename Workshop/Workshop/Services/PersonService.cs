using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Workshop.Data;
using Workshop.Extensions;
using Workshop.Models;
using Workshop.Models.Dto;
using Workshop.Models.Dto.Requests;
using Workshop.Models.Dto.Responses;
using Workshop.Services.Interfaces;

namespace Workshop.Services
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IHttpContextAccessor context;

        public PersonService(ApplicationDbContext applicationDbContext, IHttpContextAccessor context)
        {
            this.applicationDbContext = applicationDbContext;
            this.context = context;
        }

        public async Task<IEnumerable<UserInfoResponse>> GetAllCustomersInfo()
        {
            var allUsers = await applicationDbContext.Persons.Include(b => b.Books).ToListAsync();

            var persons = allUsers!.Select(user => new UserInfoResponse
            {
                Name = user.Name,
                Username = user.Username,
                BorrowedBooks = user.Books.Count
            });
            return persons;
        }

        public async Task<Person> Register(Person person)
        {
            var register = (await applicationDbContext.Persons.AddAsync(person)).Entity;
            await applicationDbContext.SaveChangesAsync();
            return register;
        }

        public async Task<Person> FindPersonByUsername(string username)
        {
            return await applicationDbContext.Persons.FirstOrDefaultAsync(p => p.Username == username);
        }
        
        public Person DoesPersonExists(string username)
        {
            return applicationDbContext.Persons.FirstOrDefault(p => p.Username == username);
        }
        
        public string GetPersonJwtUsername()
        {
            return context.HttpContext.User.Claims
                .First(i => i.Type == ClaimTypes.Name).Value;
        }

        public async Task Promote(PromoteRequest person)
        {
            var currentPerson = await FindPersonByUsername(person.Username);
            currentPerson.IsLibrarian = true;
            await applicationDbContext.SaveChangesAsync();
        }
        
        public async Task BorrowBook(BorrowInfo borrow)
        {
            await applicationDbContext.BorrowInfos.AddAsync(borrow);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}