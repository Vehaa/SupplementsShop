import { Component, OnInit } from '@angular/core';
import { Router,ActivatedRoute,Params  } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ClientsService } from '../shared/clients/clients.service';


@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styles: [
  ]
})
export class ClientsComponent implements OnInit {
  constructor(public service:ClientsService,
    private toastr:ToastrService,
    private _router:Router) { }

  ngOnInit(): void {
    this.service.getClient();
  }
  cityID:number;
  userId:number;
  

  onDelete(id:number){
    if(confirm('Da li ste sigurni da želite izbrisati?'))
    {
      this.service.deleteClient(id)
    .subscribe(
      res=>{
        this.service.getClient();
        this.toastr.error("Brisanje uspješno!","Gradovi");
      },
      err=>{console.log(err)}
    )
    }
    
  }

  onEditClient(id:number){
    this._router.navigate(['/Users/Edit',id]);
  }

  populateForm(selectedRecord){
    this.service.formData=Object.assign({},selectedRecord);
  }

  clientByName(name:string){
    this.service.getClientsByName(name);
  }
}
