using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgmaatjeWebApi.OuderVoogd.Repositories
{
    public interface IOuderVoogdRepository
    {
        Task<OuderVoogd> GetOuderVoogdByIdAsync(string id);
        Task<IEnumerable<OuderVoogd>> GetAllOuderVoogdenAsync();
        Task AddOuderVoogdAsync(OuderVoogd ouderVoogd);
        Task UpdateOuderVoogdAsync(OuderVoogd ouderVoogd);
        Task DeleteOuderVoogdAsync(string id);
    }
}

