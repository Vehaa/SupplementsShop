import { Component, OnInit } from '@angular/core';
import { Form, FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ReportsService } from 'src/app/shared/reports/reports.service';
import { Earning } from 'src/app/shared/reportsModels/earningReport.model';

@Component({
  selector: 'app-earning',
  templateUrl: './earning.component.html',
  styleUrls: [

  ]
})
export class EarningComponent implements OnInit {

  form = new FormGroup({
    dateFrom: new FormControl(''),
    dateTo: new FormControl('')
  });
  div: boolean = false;
  data: Earning;
  constructor(public service: ReportsService,
    private toastr: ToastrService) { }


  ngOnInit(): void {
  }


  onSubmit() {
    this.form.patchValue({
      dateFrom: this.form.controls['dateFrom'].value,
      dateTo: this.form.controls['dateTo'].value
    });

    this.service.getEarningReport(this.form.value).subscribe(res => {
      this.data = res as Earning;
      this.div = true;
    },

      err => {
        this.toastr.warning(err.error);
      });
  }

  onPrint() {
    window.print();
  }
}
