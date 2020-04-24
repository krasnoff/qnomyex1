import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { WaitListService } from '../wait-list.service';

@Component({
  selector: 'app-wait-list',
  templateUrl: './wait-list.component.html',
  styleUrls: ['./wait-list.component.css']
})
export class WaitListComponent implements OnInit {
  profileForm = new FormGroup({
    customerName: new FormControl(''),
  });

  onlineUser = {
    id: 0,
    ClientName: '',
    appointmentDate: '',
    isTreated: 0
  };
  fullList: any;
  waitingList: any;

  constructor(public waitListService: WaitListService) { }

  ngOnInit() {
    this._init();
  }

  private setDisplayObjects() {
    if (this.fullList.length > 0) {
      this.onlineUser = this.fullList[0];
      this.onlineUser.isTreated = 1;
      this.waitingList = this.fullList.filter(el => el.isTreated === 0);
    }
  }

  async _init() {
    this.fullList = await this.waitListService.getAppointment();
    this.setDisplayObjects();
  }

  onSubmit() {
    console.log(this.profileForm);
    const payload = {
      Id: 0,
      appointmentDate: '2019-01-06T17:16:40',
      ClientName: this.profileForm.value.customerName,
      isTreated: 0
    };
    this.waitListService.postAppointment(payload).then(data => {
      this.profileForm.value.customerName = '';
      this.fullList = data;
      this.setDisplayObjects();
    });
  }

  onUpdateQue() {
    const payload = {
      Id: this.onlineUser.id,
      appointmentDate: this.onlineUser.appointmentDate,
      ClientName: this.onlineUser.ClientName,
      isTreated: 2
    };
    this.waitListService.updateAppointment(payload).then(data => {
      this.fullList = data;
      this.setDisplayObjects();
    });
  }

}
