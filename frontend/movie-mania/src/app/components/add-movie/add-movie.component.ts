import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.scss'],
})
export class AddMovieComponent {
  movie = {
    name: '',
    description: '',
    year: 0,
    imdb: 0,
    rating: 0,
  };
  selectedFile: File | null = null;

  constructor(private http: HttpClient) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onSubmit() {
    if (!this.selectedFile) {
      alert('Please select a file.');
      return;
    }

    const formData = new FormData();
    formData.append('name', this.movie.name);
    formData.append('description', this.movie.description);
    formData.append('year', this.movie.year.toString());
    formData.append('imdb', this.movie.imdb.toString());
    formData.append('rating', this.movie.rating.toString());
    formData.append('poster', this.selectedFile);

    this.http.post('http://localhost:5067/api/movies', formData).subscribe({
      next: (response) => {
        alert('Movie added successfully!');
      },
      error: (err) => {
        console.error(err);
        alert('Error while adding movie.');
      },
    });
  }
}
