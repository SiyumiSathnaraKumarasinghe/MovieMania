import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  movies: any[] = [];

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.fetchMovies();
  }

  fetchMovies() {
    this.http.get('http://localhost:5067/api/movies').subscribe({
      next: (response: any) => {
        this.movies = response.map((movie: any) => ({
          ...movie,
          isFavorite: false, // Add a default favorite status
        }));
      },
      error: (err) => {
        console.error('Error fetching movies:', err);
      },
    });
  }

  goToMovieDetails(movieId: string) {
    this.router.navigate(['/movie-details', movieId]);
  }

  toggleFavorite(movie: any, event: Event) {
    event.stopPropagation(); // Prevent the click from triggering card navigation
    movie.isFavorite = !movie.isFavorite;
  }
}
