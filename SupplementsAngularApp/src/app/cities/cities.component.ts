import { Component, OnInit } from '@angular/core';
import { CitiesService } from '../shared/cities/cities.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styles: [
  ]
})
export class CitiesComponent implements OnInit {

  constructor(public service: CitiesService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  onDelete(id: number) {
    if (confirm('Da li ste sigurni da želite izbrisati?')) {
      this.service.deleteCity(id)
        .subscribe(
          res => {
            this.service.refreshList();
            this.toastr.error("Brisanje uspješno!", "Gradovi");
          },
          err => { this.toastr.error(err.error); }
        )
    }

  }

}
