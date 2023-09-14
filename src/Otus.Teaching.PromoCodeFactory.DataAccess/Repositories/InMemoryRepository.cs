using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>
        : IRepository<T>
        where T: BaseEntity
    {
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }
        
        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public async Task DeleteAsync(Employee employee)
        {
            IEnumerable<T> employees = await GetAllAsync();
            Data = employees.Where(e => e.Id != employee.Id);
            await Task.CompletedTask;
        }

        public async Task CreateAsync(Employee employee)
        {
            IEnumerable<T> employees = await GetAllAsync();
            List<Employee> employeeList = employees.Cast<Employee>().ToList();
            employeeList.Add(employee);
            employees = (IEnumerable<T>)employeeList;
            Data = employees;
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(T employee)
        {
            var temp = Data.FirstOrDefault(e => e.Id != employee.Id);
            temp = employee;
            await Task.CompletedTask;
        }

    }
}