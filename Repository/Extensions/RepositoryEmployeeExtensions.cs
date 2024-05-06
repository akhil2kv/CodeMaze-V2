using Entities.Models;
using Repository.Extensions.Utilities;
using System.Reflection;
using System.Text;

namespace Repository.Extensions
{
    public static class RepositoryEmployeeExtensions
    {
        public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint minAge, uint maxAge)
            => employees.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<Employee> Search (this IQueryable<Employee> employees,string searchItem)
        {
            if(string.IsNullOrWhiteSpace(searchItem))
                return employees;

            var lowerCaseSearchItem = searchItem.Trim().ToLower();

            return employees.Where(e => e.Name.ToLower().Contains(lowerCaseSearchItem));
        }

        public static IQueryable<Employee> Sort(this IQueryable<Employee> employees,string orderbyQueryString)
        {
            if(string.IsNullOrWhiteSpace(orderbyQueryString))
                return employees.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderbyQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return employees.OrderBy(e => e.Name);

            return employees.OrderBy(e => orderQuery);
        }
    }
}
