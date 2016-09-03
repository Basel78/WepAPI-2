using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace BackendService.Model
{
    public class UnitOfWork : IDisposable
    {
        private EFProvider.MyDbContext context = new EFProvider.MyDbContext();

        private Repository<Team> TeamRepository;
        private Repository<Match> MatchRepository;

        public Repository<Team> TeamsRepository
        {
            get 
            {
                if (this.TeamRepository == null)
                {
                    this.TeamRepository = new Repository<Team>(context);
                }
                return TeamRepository;
            }
        }

        public Repository<Match> MatchsRepository
        {
            get 
            {
                if (this.MatchRepository == null)
                {
                    this.MatchRepository = new Repository<Match>(context);
                }
                return MatchRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
