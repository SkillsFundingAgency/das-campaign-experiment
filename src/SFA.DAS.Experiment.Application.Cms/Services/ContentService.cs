using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Search;
using SFA.DAS.Experiment.Application.Cms.ContentTypes;

namespace SFA.DAS.Experiment.Application.Cms.Services
{
    public class ContentService : IContentService
    {
        private readonly ContentfulClient _contentfulClient;

        public ContentService(ContentfulClient contentfulClient)
{
            _contentfulClient = contentfulClient;
        }

        public async Task<List<T>> GetEntriesByType<T>() where T : IContentType
        {
            var builder = new QueryBuilder<T>().ContentTypeIs(typeof(T).Name.FirstCharacterToLower()).Include(0);
            return (await _contentfulClient.GetEntries(builder)).ToList();
        }

        public async Task<T> GetEntry<T>(string id)
        {
             return await _contentfulClient.GetEntry<T>(id);
        }
    }

    public interface IContentService
    {
        Task<List<T>> GetEntriesByType<T>() where T : IContentType;
        Task<T> GetEntry<T>(string id);
    }

    public static class StringExtensions
    {
        public static string FirstCharacterToLower(this string input)
        {
            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }
    }
}