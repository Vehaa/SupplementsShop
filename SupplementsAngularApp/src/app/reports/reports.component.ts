import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReportsService } from '../shared/reports/reports.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: [
    
  ]
})
export class ReportsComponent implements OnInit {

  constructor(reportService:ReportsService,
    private _router:Router) { }

  ngOnInit(): void {
  }

  earning(){
    this._router.navigate(['/Reports/Earning']);
  }

  topProducts(){
    this._router.navigate(['/Reports/TopProducts']);
  }

  topCustomers(){
    this._router.navigate(['/Reports/TopCustomers']);
  }

}
