using Hesira.Entities;

namespace Hesira.Repositories.Nucleus.Interfaces
{

    // create entities context
    public interface IDBFactory
    {
        HesiraEntities Create();
    }
}
