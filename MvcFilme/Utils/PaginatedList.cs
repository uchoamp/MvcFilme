using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcFilme.Utils
{
    /// <summary>
    ///  Class <c>PaginatedList</c> cria uma lista com paginação e ajuda na criação do template da paginação
    /// </summary>
    /// <typeparam name="T">O tipo da entidade a ser página</typeparam>
    public class PaginatedList<T>: List<T>
    {
        /// <summary>
        /// Página atual
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// Total páginas
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Cria uma instância de <c>PaginatedList</c> mas não faz a paginação em si
        /// </summary>
        /// <param name="items">Items paginados</param>
        /// <param name="count">Quantidade total de items</param>
        /// <param name="pageIndex">Página atual</param>
        /// <param name="pageSize">Quantidade de items por página</param>
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count/(double)pageSize);

            this.AddRange(items);
        }

        /// <summary>
        /// Checa se existi uma página anterior
        /// </summary>
        public bool HasPreviosPage => PageIndex > 1;

        /// <summary>
        /// Checa se existi uma próxima página 
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;

        /// <summary>
        /// Interagem com as próximas n páginas anterior a atual 
        /// </summary>
        /// <param name="limit">Quantidade de páginas visiveis a partir da página atual</param>
        /// <returns>Interador com inteiro</returns>
        public IEnumerable<int> PreviousNeighbors(int limit)
        {
            if(PageIndex == 1) 
                yield break;

            for (int i = limit; i > 0; i--)
                if (0 < (PageIndex - i))
                    yield return PageIndex - i;

            yield break;
        }

        /// <summary>
        /// Interagem com as próximas n páginas mas posterior a atual 
        /// </summary>
        /// <param name="limit">Quantidade de páginas visiveis a partir da página atual</param>
        /// <returns>Interador de inteiros</returns>
        public IEnumerable<int> NextNeighbors(int limit)
        {
            if(PageIndex == TotalPages) 
                yield break;

            for (int i = 1; (i+PageIndex) <= TotalPages && i < limit; i++)
                yield return PageIndex + i;
            
            yield break;
        }

        /// <summary>
        /// Cria uma uma lista páginada de forma asincronar
        /// </summary>
        /// <param name="source">Query utilizada para a paginação</param>
        /// <param name="pageIndex">Página atual</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <returns>Uma lista páginada</returns>
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex-1)*pageSize).Take(pageSize).ToListAsync(); 
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
