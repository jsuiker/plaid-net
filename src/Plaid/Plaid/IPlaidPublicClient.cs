using System.Threading.Tasks;
using Plaid.Contracts;

namespace Plaid
{
    public interface IPlaidPublicClient
    {
        /// <summary>
        /// Gets the institutions.
        /// </summary>
        /// <returns></returns>
        Task<Response<Institution[]>> GetInstitutions();

        /// <summary>
        /// Gets the institution.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Response<Institution>> GetInstitution(string id);

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        Task<Response<Category[]>> GetCategories();

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Response<Category>> GetCategory(string id);
    }
}