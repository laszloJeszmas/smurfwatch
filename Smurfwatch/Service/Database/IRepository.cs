using Functional.Maybe;
using System.Collections.Generic;

namespace Smurfwatch.Service.Database
{
    public interface IRepository<T>
    {
        Maybe<T> GetSingle(Query query);
        IList<T> GetMultiple(Query query);
        void Add(Query query);
        void Delete(Query query);
        B AddAndGetId<B>(Query query);
    }
}
