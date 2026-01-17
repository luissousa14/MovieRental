using MovieRental.Data;

namespace MovieRental.Movie
{
	public class MovieFeatures : IMovieFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public MovieFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}
		
		public Movie Save(Movie movie)
		{
			_movieRentalDb.Movies.Add(movie);
			_movieRentalDb.SaveChanges();
			return movie;
		}

        // TODO: tell us what is wrong in this method? Forget about the async, what other concerns do you have?
        //Não tem qualquer forma de apanhar as exceções que podem ocorrer durante a consulta ao banco de dados.
        //Além disso, não há paginação ou filtragem, o que pode ser problemático para grandes conjuntos de dados.
        public List<Movie> GetAll()
		{
			return _movieRentalDb.Movies.ToList();
		}


	}
}
