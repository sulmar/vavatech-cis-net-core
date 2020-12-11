using Bogus;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;
using Vavatech.CIS.Models.SearchCriterias;

namespace Vavatech.CIS.DbServices
{
    // dotnet add package Dapper

    public class DbCustomerService : ICustomerService
    {
        private readonly IDbConnection connection;

        private readonly Faker<Customer> customerFaker;

        public DbCustomerService(IDbConnection connection, Faker<Customer> customerFaker)
        {
            this.connection = connection;

            //var customers = customerFaker.Generate(100);
            //AddRange(customers);
        }

        private void AddRange(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Add(customer);
            }
        }

        public void Add(Customer customer)
        {
            string sql = @"INSERT INTO [dbo].[Customers]
                           ([FirstName]
                           ,[LastName]
                           ,[Gender]
                           ,[Email]
                           ,[Username]
                           ,[HashedPassword]
                           ,[Pesel]
                           ,[Salary]
                           ,[IsRemoved])
                     VALUES (
                           @FirstName, 
                           @LastName, 
                           @Gender, 
                           @Email, 
                           @Username, 
                           @HashedPassword,
                           @Pesel, 
                           @Salary,
                           @IsRemoved)";


            int affectedRows = connection.Execute(sql, new
            {
                customer.FirstName,
                customer.LastName,
                @Gender = customer.Gender.ToString(),
                customer.Email,
                customer.Username,
                customer.HashedPassword,
                customer.Pesel,
                customer.Salary,
                customer.IsRemoved
            });

        }

        public IEnumerable<Customer> Get()
        {
            string sql = @"SELECT 
                               [CustomerId] as [Id]
                              ,[FirstName]
                              ,[LastName]
                              ,[Gender]
                              ,[Email]
                              ,[Username]
                              ,[HashedPassword]
                              ,[Pesel]
                              ,[Salary]
                              ,[IsRemoved]
                          FROM [CISDb].[dbo].[Customers]";

            var customers = connection.Query<Customer>(sql).ToList();

            return customers;
        }

        public Customer Get(int id)
        {
            string sql = @"SELECT 
                               [CustomerId] as [Id]
                              ,[FirstName]
                              ,[LastName]
                              ,[Gender]
                              ,[Email]
                              ,[Username]
                              ,[HashedPassword]
                              ,[Pesel]
                              ,[Salary]
                              ,[IsRemoved]
                          FROM [CISDb].[dbo].[Customers]
                          WHERE CustomerId = @CustomerId";

            Customer customer = connection.QuerySingleOrDefault<Customer>(sql, new { @CustomerId = id });

            return customer;
        }

        public IEnumerable<Customer> Get(Gender gender)
        {
            string sql = @"SELECT 
                               [CustomerId] as [Id]
                              ,[FirstName]
                              ,[LastName]
                              ,[Gender]
                              ,[Email]
                              ,[Username]
                              ,[HashedPassword]
                              ,[Pesel]
                              ,[Salary]
                              ,[IsRemoved]
                          FROM [CISDb].[dbo].[Customers]
                          WHERE Gender = @Gender";

            var customers = connection.Query<Customer>(sql, new { @Gender = gender.ToString() }).ToList();

            return customers;
        }

        public IEnumerable<Customer> Get(string lastName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get(string firstname, decimal? from, decimal? to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            string sql = @"SELECT 
                               [CustomerId] as [Id]
                              ,[FirstName]
                              ,[LastName]
                              ,[Gender]
                              ,[Email]
                              ,[Username]
                              ,[HashedPassword]
                              ,[Pesel]
                              ,[Salary]
                              ,[IsRemoved]
                          FROM [CISDb].[dbo].[Customers]

                        /**where**/  

                        ";

            // dotnet add package Dapper.SqlBuilder
            var builder = new SqlBuilder();
            var template = builder.AddTemplate(sql);

            if (!string.IsNullOrEmpty(searchCriteria.FirstName))
                builder.Where("FirstName like '%' + @FirstName + '%'", new { searchCriteria.FirstName });

            if (searchCriteria.From.HasValue)
                builder.Where("Salary >= @From", new { @From = searchCriteria.From });

            if (searchCriteria.To.HasValue)
                builder.Where("Salary < @To", new { @To = searchCriteria.To });

            var customers = connection.Query<Customer>(template.RawSql, template.Parameters);

            return customers;
        }

        public Customer GetByPesel(string number)
        {
            throw new NotImplementedException();
        }

        public Customer GetByUsername(string username)
        {
            string sql = @"SELECT 
                               [CustomerId] as [Id]
                              ,[FirstName]
                              ,[LastName]
                              ,[Gender]
                              ,[Email]
                              ,[Username]
                              ,[HashedPassword]
                              ,[Pesel]
                              ,[Salary]
                              ,[IsRemoved]
                          FROM [CISDb].[dbo].[Customers]
                          WHERE Username = @Username";

            return connection.QuerySingleOrDefault<Customer>(sql, new { @Username = username });
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
