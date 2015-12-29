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
        Task<Institution[]> GetInstitutions();

        /// <summary>
        /// Gets the institution.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Institution> GetInstitution(string id);

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        Task<Category[]> GetCategories();

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Category> GetCategory(string id);
    }
}