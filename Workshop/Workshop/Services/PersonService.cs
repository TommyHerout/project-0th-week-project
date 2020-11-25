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

namespace Workshop.Services
{
    public class PersonService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IHttpContextAccessor context;

        public PersonService(ApplicationDbContext applicationDbContext, IHttpContextAccessor context)
        {
            this.applicationDbContext = applicationDbContext;
            this.context = context;
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
        
        public async Task<Person> FindPersonById(int personId)
        {
            return await applicationDbContext.Persons.FirstOrDefaultAsync(p => p.Id == personId);
        }

        public string GetPersonJwtUsername()
        {
            return context.HttpContext.User.Claims
                .First(i => i.Type == ClaimTypes.Name).Value;
        }

        public async Task Promote(PromoteRequest person)
        {
            var currentPerson = await FindPersonByUsername(person.Username);
            currentPerson.IsLibrarian ^= true;
            await applicationDbContext.SaveChangesAsync();
        }
        
        public async Task BorrowBook(BorrowInfo borrow)
        {
            await applicationDbContext.BorrowInfos.AddAsync(borrow);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}