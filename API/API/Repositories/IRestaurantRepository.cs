using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Repositories
{
    public interface IRestaurantRepository
    {
        #region General

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        #endregion

        #region Customers

        Task<Customer[]> GetAllCustomersAsync();
        Task<Customer> GetCustomerById(int id);
        Task<Customer[]> GetAllCustomersSprocAsync();
        Task<Customer> GetCustomerByIdSprocAsync(int id);

        #endregion

        #region Orders

        Task<Order[]> GetOrdersAsync();

        #endregion

        #region OrderDetails

        Task<OrderDetail[]> GetOrderDetailsAsync();

        #endregion
    }
}
