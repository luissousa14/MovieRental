using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.PaymentProviders;
using System.Security.Principal;
using System.Threading;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public RentalFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}

        //TODO: make me async :( - DONE
        //Um método assíncrono(async) permite que a aplicação continue a executar outras tarefas enquanto espera que uma operação demorada termine(como acesso à base de dados).
        //Um método síncrono bloqueia a execução até essa operação acabar.
        //Diferença principal:
        //	Síncrono: bloqueia a thread.
        //	Assíncrono: não bloqueia a thread.

        public async Task<Rental> Save(Rental rental)
		{

            var provider = PaymentProviderFactory.GetProvider(rental.PaymentMethod);

            bool paymentSuccess = await provider.Pay(rental.Price);

            if (!paymentSuccess)
            {
                throw new Exception("Payment failed");
            }

            await _movieRentalDb.Rentals.AddAsync(rental);
			await _movieRentalDb.SaveChangesAsync();
            return await _movieRentalDb.Rentals.Include(r => r.Movie)
                                                .Include(r => r.Customer)
                                                .FirstAsync(r => r.Id == rental.Id);
        }

		//TODO: finish this method and create an endpoint for it
		public async Task<IEnumerable<Rental>> GetRentalsByCustomerName(string customerName)
		{
			var rentalsByCustomerName = await _movieRentalDb.Rentals
				.Include(r => r.Customer)
                .Where(r => r.Customer != null && r.Customer.CustomerName.Contains(customerName))
				.ToListAsync();

            return rentalsByCustomerName;

        }
        //A melhor forma de lidar com exceptions é usar um tratamento global de erros, registar os problemas e devolver respostas HTTP claras sem expor detalhes internos.
        public async Task<IEnumerable<Rental>> GetRentals()
        {
            var rentals = await _movieRentalDb.Rentals
				.Include(r => r.Customer)
				.Include(r => r.Movie)
                .ToListAsync();

            return rentals;

        }

    }
}
